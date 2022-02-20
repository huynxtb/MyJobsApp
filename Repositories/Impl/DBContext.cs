using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MyJobsApp.Repositories.Impl
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetSqlConnection()
            => new SqlConnection(_configuration.GetConnectionString("JobsBackupInformation"));
    }
}