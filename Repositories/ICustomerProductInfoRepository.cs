using System.Collections.Generic;
using System.Threading.Tasks;
using MyJobsApp.Models;

namespace MyJobsApp.Repositories
{
    public interface ICustomerProductInfoRepository
    {
        Task<List<CustomerProductInfoDetailViewModel>>  GetAll();
        Task<CustomerProductInfoDetailViewModel> FindOneByEmail(string email);
        Task<CustomerProductInfoDetailViewModel> FindOneByDatabaseName(string databaseName);
    }
}