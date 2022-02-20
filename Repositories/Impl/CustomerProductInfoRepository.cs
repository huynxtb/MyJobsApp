using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyJobsApp.Models;
using Microsoft.Extensions.Configuration;
using MyJobsApp.Constants;
using Newtonsoft.Json;

namespace MyJobsApp.Repositories.Impl
{
    public class CustomerProductInfoRepository : DBContext, ICustomerProductInfoRepository
    {
        public CustomerProductInfoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<CustomerProductInfoDetailViewModel>> GetAll()
        {
            using (var conn = GetSqlConnection())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                
                var parameters = new DynamicParameters();

                parameters.Add(StoredVariable.UserId, "userId");
                parameters.Add(StoredVariable.Action, StoredAction.GetAll);
                parameters.Add(StoredVariable.JInput, JsonConvert.SerializeObject(new { }));

                var res = await conn.QueryAsync<CustomerProductInfoDetailViewModel>(
                        StoredProcedureName.UpsInsUpdCustomerProdInfo,
                        parameters,
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
                
                return res.AsList();
            }
        }

        public async Task<CustomerProductInfoDetailViewModel> FindOneByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CustomerProductInfoDetailViewModel> FindOneByDatabaseName(string databaseName)
        {
            throw new System.NotImplementedException();
        }
    }
}