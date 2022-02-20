using System.Collections.Generic;
using System.Threading.Tasks;
using MyJobsApp.Models;
using MyJobsApp.Repositories;

namespace MyJobsApp.Services.Impl
{
    public class BackupFileService : IBackupFileService
    {
        private readonly IBackupFileRepository _backupFileRepository;

        public BackupFileService(IBackupFileRepository backupFileRepository)
        {
            _backupFileRepository = backupFileRepository;
        }
        
        public async Task<ResponseViewModel> AddNewBackupFile(AddNewFileBackupViewModel model, string userId)
        {
            return await _backupFileRepository.AddNewBackupFile(model, userId);
        }

        public async Task<List<BackupFileDetailViewModel>> GetAllBackupFile()
        {
            throw new System.NotImplementedException();
        }

        public async Task<BackupFileDetailViewModel> BackupFileDetail(string backupFileId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseViewModel> DeleteBackupFile(string[] backupFileId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<BackupFileDetailViewModel>> CleanUpBackupFile(string databaseName)
        {
            return await _backupFileRepository.CleanUpBackupFile(databaseName);
        }
    }
}