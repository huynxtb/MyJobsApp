using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyJobsApp.Models;

namespace MyJobsApp.Services
{
    public interface ICommonService
    {
        Task<string> UploadFileToMega(byte[] fileBytes, string fileName, string megaFolderName);
        Task<FileStreamResult> DownloadFileFromMega(string url, string contentType);
        Task DeleteFileFromMega(string url);
        Task SendSingleMail(EmailMessageViewModel dto);
    }
}