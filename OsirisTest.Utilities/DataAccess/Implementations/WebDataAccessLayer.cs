using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OsirisTest.Contracts.ResponseModels;
using OsirisTest.Utilities.DataAccess.DataContracts;

namespace OsirisTest.Utilities.DataAccess.Implementations
{
    public class WebDataAccessLayer : IWebDataAccessLayer
    {
        private static readonly List<int> LockedCustomers = new List<int>
        {
            1,7,16,21,28,29,36,48,49,56,70,88,91,92,99
        };

        public Task<bool> IsLockedCustomer(int customerId)
        {
            return Task.FromResult(LockedCustomers.Contains(customerId));
        }

        public Task<CustomerResponse> GetCustomerById(int customerId)
        {
            return Task.FromResult(
                    new CustomerResponse
                    {
                        CustomerId = customerId,
                        DateOfBirth = DateTime.Now.AddYears(customerId < 18 ? -customerId : -(18 + customerId)),
                        FirstDepositAmount = 10 * customerId,
                        FirstName = $"Firstname{customerId}",
                        LastName = $"LastName{customerId}",
                        EmailAddress = $"MailAddress{customerId}@ottest.com",
                        LastUpdateDateTime = DateTime.Now
                    }
                );
        }
    }
}
