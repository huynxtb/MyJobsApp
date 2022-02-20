using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MyJobsApp.Constants;
using MyJobsApp.Helper;
using MyJobsApp.Models;
using MyJobsApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MyJobsApp.Repositories;

namespace MyJobsApp.JobSchedule
{
    public class MyJobSchedule : IMyJobSchedule
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly ICommonService _commonService;
        private readonly IBackupFileService _backupFileService;
        private readonly ICustomerProductInfoRepository _customer;

        public MyJobSchedule(IWebHostEnvironment webHostEnvironment
            , IConfiguration configuration
            , ICommonService commonService
            , IBackupFileService backupFileService
            , ICustomerProductInfoRepository customer)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _commonService = commonService;
            _backupFileService = backupFileService;
            _customer = customer;
        }

        public async Task DailyBackupMaterialStore()
        {
            Console.WriteLine(
                $"Backup connection database : {_configuration.GetConnectionString("MaterialStore")} is running...");

            var contentRootPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(contentRootPath, SystemConst.BackupSrcFolder);

            string fileName = SqlBackupHelper.SaveFileBackUpToServer(path
                , _configuration.GetConnectionString("MaterialStore")
                , _configuration["DatabasesBackup:MaterialStore"]);

            if (!string.IsNullOrEmpty(fileName))
            {
                byte[] fileBytes = File.ReadAllBytes(path + fileName);

                var downloadLink =
                    await _commonService.UploadFileToMega(fileBytes, fileName, SystemConst.MaterialStoreBackup);

                var model = new AddNewFileBackupViewModel()
                {
                    BackupFileId = Guid.NewGuid().ToString(),
                    Website = "https://www.nhuahunghanh.com/",
                    DatabaseName = _configuration["DatabasesBackup:MaterialStore"],
                    BackupFileName = fileName,
                    BackupFileUrl = downloadLink
                };

                if (!string.IsNullOrEmpty(downloadLink))
                {
                    await _backupFileService.AddNewBackupFile(model,
                        _configuration["DatabasesBackup:MaterialStore"].ToLower());
                }

                File.Delete(path + fileName);
            }
        }

        public async Task SendMailPaymentServer20ThOfEveryMonth()
        {
            var listCus = await _customer.GetAll();
            var contentRootPath = _webHostEnvironment.WebRootPath;
            var header = Path.Combine(contentRootPath, "EmailTemplate/CustomerServerInvoiceHeader.html");
            var body = Path.Combine(contentRootPath, "EmailTemplate/CustomerServerInvoiceBody.html");
            var cul = CultureInfo.GetCultureInfo("vi-VN"); // try with "en-US"
            var subject = "NHẮC NHỞ-THANH TOÁN SERVER WEBSITE";

            foreach (var cus in listCus)
            {
                var emailContent = await EmailHelper
                    .EmailTemplateBuilder(
                        header,
                        body,
                        cus.CustomerName.ToUpper(), //0
                        cus.Website, //1
                        cus.Website.Replace("https://www.", string.Empty), //2
                        OwnerInfo.EmailAddress, //3
                        OwnerInfo.EmailAddress, //4
                        $"{cus.ExpirationDate}/{DateTime.Now.Month}/{DateTime.Now.Year}", //5
                        $"{cus.ExpirationDate}/{DateTime.Now.Month}/{DateTime.Now.Year}", //6
                        OwnerInfo.Bank, //7
                        OwnerInfo.BankNumber, //8
                        cus.PaymentAmount.ToString("#,###", cul.NumberFormat), //9
                        cus.Website.Replace("https://www.", string.Empty), //10
                        cus.VpsLocation, //11
                        cus.VpsIPAddress, //12
                        cus.VpsCPU, //13
                        cus.VpsRam, //14
                        cus.VpsTransfer, //15
                        cus.VpsStorage, //16
                        OwnerInfo.Signature, //17
                        OwnerInfo.EmailAddress, //18
                        OwnerInfo.PhoneNumber, //19
                        OwnerInfo.PhoneNumber, //20
                        DateTime.Now.Year, //21
                        OwnerInfo.Website, //22
                        OwnerInfo.Website.Replace("https://www.", string.Empty), //23
                        cus.Website.Replace("https://www.", string.Empty), //24
                        "https://www.hawkhost.com/", //25
                        "HawkHost", //26
                        OwnerInfo.BankOwnerName); //27

                await _commonService.SendSingleMail(new EmailMessageViewModel()
                {
                    Content = emailContent,
                    Subject = subject,
                    SendTos = new List<string>() {cus.Email}
                });
            }
        }

        public async Task CleanUpBackupMaterialStore()
        {
            var listToDelete =
                await _backupFileService.CleanUpBackupFile(_configuration["DatabasesBackup:MaterialStore"]);

            foreach (var item in listToDelete.Where(item => !string.IsNullOrEmpty(item.BackupFileUrl)))
            {
                await _commonService.DeleteFileFromMega(item.BackupFileUrl);
            }
        }
    }
}