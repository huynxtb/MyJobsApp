using System.Collections.Generic;
using System.Threading.Tasks;
using MyJobsApp.Models;
using MyJobsApp.Repositories;

namespace MyJobsApp.Services.Impl
{
    public class CustomerProductInfoService : ICustomerProductInfoService
    {
        private readonly ICustomerProductInfoRepository _customer;

        public CustomerProductInfoService(ICustomerProductInfoRepository customer)
        {
            _customer = customer;
        }

        public async Task<List<CustomerProductInfoDetailViewModel>> GetAll()
        {
            return await _customer.GetAll();
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