using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyJobsApp.JobSchedule;

namespace MyJobsApp.Controllers
{
    [Route("job")]
    public class JobController : BaseController
    {
        private readonly IMyJobSchedule _myJobSchedule;

        public JobController(IMyJobSchedule myJobSchedule)
        {
            _myJobSchedule = myJobSchedule;
        }

        [Route("backup/daily-back-up-material-store")]
        public async Task<IActionResult> DailyBackupMaterialStore()
        {
            try
            {
                await _myJobSchedule.DailyBackupMaterialStore();

                return SuccessResponses("backup/daily-back-up-material-store");
            }
            catch (Exception e)
            {
                return ErrorResponse(e.Message);
            }
        }
        
        [Route("send-mail/monthly-payment-web-server")]
        public async Task<IActionResult> SendMailPaymentServer20ThOfEveryMonth()
        {
            try
            {
                await _myJobSchedule.SendMailPaymentServer20ThOfEveryMonth();

                return SuccessResponses("mail/monthly-payment-web-server");
            }
            catch (Exception e)
            {
                return ErrorResponse(e.Message);
            }
        }
        
        [Route("backup/clean-file-back-up-material-store")]
        public async Task<IActionResult> CleanUpBackupMaterialStore()
        {
            try
            {
                await _myJobSchedule.CleanUpBackupMaterialStore();

                return SuccessResponses("backup/clean-file-back-up-material-store");
            }
            catch (Exception e)
            {
                return ErrorResponse(e.Message);
            }
        }
    }
}