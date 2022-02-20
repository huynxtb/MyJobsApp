using System.Threading.Tasks;

namespace MyJobsApp.JobSchedule
{
    public interface IMyJobSchedule
    {
        Task DailyBackupMaterialStore();
        Task SendMailPaymentServer20ThOfEveryMonth();
        Task CleanUpBackupMaterialStore();
    }
}