using OsirisTest.Utilities.DataAccess.Models;

namespace OsirisTest.Utilities.DataAccess.DataContracts
{
    public interface IConsumerAccessLayer
    {
        Customer SaveOrUpdateCustomer(Customer customer);

        Wager SaveOrUpdateWager(Wager wager, bool isValid);

        void UpdateCustomerLastWager(CustomerLastWager lastWager);
    }

}
