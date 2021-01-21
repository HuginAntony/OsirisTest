using System.Threading.Tasks;
using OsirisTest.Contracts.ResponseModels;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.WebApi.BusinessLayer.DataContracts;

namespace OsirisTest.WebApi.BusinessLayer.Implementations
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IWebDataAccessLayer _WebDataAccessLayer;
        public CustomerManager(IWebDataAccessLayer webAccessLayer)
        {
            _WebDataAccessLayer = webAccessLayer;
        }
        public Task<bool> IsLockedCustomer(int customerId)
        {
            return _WebDataAccessLayer.IsLockedCustomer(customerId);
        }

        public Task<CustomerResponse> GetCustomerById(int customerId)
        {
            return _WebDataAccessLayer.GetCustomerById(customerId);
        }
    }
}
