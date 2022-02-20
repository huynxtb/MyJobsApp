using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MyJobsApp.Constants;
using MyJobsApp.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MyJobsApp.Repositories.Impl
{
    public class BackupFileRepository : DBContext, IBackupFileRepository
    {
        public async Task<ResponseViewModel> AddNewBackupFile(AddNewFileBackupViewModel model, string userId)
        {
            using (var conn = GetSqlConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                
                var parameters = new DynamicParameters();

                parameters.Add(StoredVariable.UserId, userId);
                parameters.Add(StoredVariable.Action, StoredAction.Insert);
                parameters.Add(StoredVariable.JInput, JsonConvert.SerializeObject(model));

                var res = await conn.QueryFirstOrDefaultAsync<ResponseViewModel>(
                        StoredProcedureName.UpsInsUpdBackupFile,
                        parameters,
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
                
                return res;
            }
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
            using (var conn = GetSqlConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                var parameters = new DynamicParameters();

                parameters.Add(StoredVariable.UserId, databaseName);
                parameters.Add(StoredVariable.Action, StoredAction.CleanUp);
                parameters.Add(StoredVariable.JInput, JsonConvert.SerializeObject(new { DatabaseName = databaseName }));
                
                var res = await conn.QueryAsync<BackupFileDetailViewModel>(
                        StoredProcedureName.UpsInsUpdBackupFile,
                        parameters,
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
                
                return res.AsList();
            }
        }

        public BackupFileRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}