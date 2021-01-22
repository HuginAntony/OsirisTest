using System;
using System.Threading.Tasks;
using OsirisTest.Utilities.DataAccess.Models;

namespace OsirisTest.Utilities.DataAccess.DataContracts
{
    public interface IConsumerAccessLayer
    {
        Task<Customer> SaveOrUpdateCustomer(Customer customer);
        Task<bool> IsValidCustomer(int customerId);
        Task<Wager> SaveOrUpdateWager(Wager wager, bool isValid);
        void UpdateCustomerLastWager(CustomerLastWager lastWager);
        void UpdateLastEmailDate(int customerId);
        Task<DateTime> GetLastEmailDate(int customerId);
    }
}
