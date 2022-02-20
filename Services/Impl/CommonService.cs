using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MyJobsApp.Constants;
using CG.Web.MegaApiClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyJobsApp.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MyJobsApp.Services.Impl
{
    public class CommonService : ICommonService
    {
        private readonly IConfiguration _configuration;

        public CommonService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadFileToMega(byte[] fileBytes, string fileName, string megaFolderName)
        {
            using (var memoryStream = new MemoryStream(fileBytes))
            {
                var megaClient = new MegaApiClient();

                await megaClient.LoginAsync(_configuration["MegaAPI:Email"], _configuration["MegaAPI:Password"]);
                var nodes = await megaClient.GetNodesAsync();
                var root = nodes.First(x => x.Type == NodeType.Directory && x.Name == megaFolderName);

                if (root == null)
                {
                    throw new Exception("Mega folder not found!");
                }

                var myFile = await megaClient.UploadAsync(memoryStream, fileName, root);
                var megaUrl = await megaClient.GetDownloadLinkAsync(myFile);
                await megaClient.LogoutAsync();

                return megaUrl.ToString();
            }
        }

        public async Task<FileStreamResult> DownloadFileFromMega(string url, string contentType)
        {
            var megaClient = new MegaApiClient();
            await megaClient.LoginAnonymousAsync();

            var fileLink = new Uri(url);
            var node = await megaClient.GetNodeFromLinkAsync(fileLink);

            var stream = await megaClient.DownloadAsync(fileLink);

            await megaClient.LogoutAsync();

            return new FileStreamResult(stream, contentType)
            {
                FileDownloadName = node.Name
            };
        }

        public async Task DeleteFileFromMega(string url)
        {
            var megaClient = new MegaApiClient();

            await megaClient.LoginAsync(_configuration["MegaAPI:Email"], _configuration["MegaAPI:Password"]);

            var fileLink = new Uri(url);
            var node = await megaClient.GetNodeFromLinkAsync(fileLink);

            var nodes = await megaClient.GetNodesAsync();
            var allFiles = nodes.Where(n => n.Type == NodeType.File).ToList();
            var myFile = allFiles.FirstOrDefault(f => f.Name == node.Name);

            await megaClient.DeleteAsync(myFile, false);

            await megaClient.LogoutAsync();
        }

        public async Task<bool> SendSingleMail(EmailMessageViewModel dto)
        {
            var client = new SendGridClient(_configuration["SendGrid:AccessToken"]);
            var from = new EmailAddress(_configuration["SendGrid:From"], _configuration["SendGrid:Name"]);
            var to = new EmailAddress(dto.SendTos.FirstOrDefault(), "");
            var msg = MailHelper.CreateSingleEmail(from, to, dto.Subject, "", dto.Content);
            var response = await client.SendEmailAsync(msg);
            
            return response.IsSuccessStatusCode;
        }
    }
}