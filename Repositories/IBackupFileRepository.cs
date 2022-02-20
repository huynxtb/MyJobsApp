using System.Collections.Generic;
using System.Threading.Tasks;
using MyJobsApp.Models;

namespace MyJobsApp.Repositories
{
    public interface IBackupFileRepository
    {
        Task<ResponseViewModel> AddNewBackupFile(AddNewFileBackupViewModel model, string userId);
        Task<List<BackupFileDetailViewModel>> GetAllBackupFile();
        Task<BackupFileDetailViewModel> BackupFileDetail(string backupFileId);
        Task<ResponseViewModel> DeleteBackupFile(string[] backupFileId);
        Task<List<BackupFileDetailViewModel>> CleanUpBackupFile(string databaseName);
    }
}