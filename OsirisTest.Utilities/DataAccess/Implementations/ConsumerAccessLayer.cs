using System;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Models;

namespace OsirisTest.Utilities.DataAccess.Implementations
{
    public class ConsumerAccessLayer: IConsumerAccessLayer
    {
        /*******************************************************************************************************************/
        // You are free to use any ORM you feel comfortable with for database manipulation
        // You may update the request models and data access interfaces as you wish in order to achieve your end goal
        /******************************************************************************************************************/
        public Customer SaveOrUpdateCustomer()
        {
            //TODO: Implement database save function that will save and return the saved customer
            throw new NotImplementedException("Implement database save function that will save and return the saved customer");
        }

        public Wager SaveOrUpdateWager()
        {
            //TODO: Implement database save function that will save and return the saved wager
            throw new NotImplementedException("Implement database save function that will save and return the saved wager");
        }

        public void UpdateCustomerLastWager()
        {
            //TODO: Implement database save function that will updated customer last wager date and amount
            throw new NotImplementedException("Implement database save function that will updated customer last wager date and amount");
        }
    }
}
