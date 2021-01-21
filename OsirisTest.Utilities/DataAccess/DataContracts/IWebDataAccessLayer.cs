using System.Threading.Tasks;
using OsirisTest.Contracts.ResponseModels;

namespace OsirisTest.Utilities.DataAccess.DataContracts
{
    public interface IWebDataAccessLayer
    {
        Task<Response> IsLockedCustomer(int customerId);

        Task<CustomerResponse> GetCustomerById(int customerId);
    }

}
