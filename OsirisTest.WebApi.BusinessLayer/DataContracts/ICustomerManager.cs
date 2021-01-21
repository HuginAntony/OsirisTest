using System.Threading.Tasks;
using OsirisTest.Contracts.ResponseModels;

namespace OsirisTest.WebApi.BusinessLayer.DataContracts
{
    public interface ICustomerManager
    {
        Task<Response> IsLockedCustomer(int customerId);

        Task<CustomerResponse> GetCustomerById(int customerId);
    }
}
