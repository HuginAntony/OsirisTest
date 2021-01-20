using System;
using System.Linq;
using OsirisTest.Data;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Models;
using Customer = OsirisTest.Utilities.DataAccess.Models.Customer;
using Wager = OsirisTest.Utilities.DataAccess.Models.Wager;

namespace OsirisTest.Utilities.DataAccess.Implementations
{
    public class ConsumerAccessLayer: IConsumerAccessLayer
    {
        private OsirisContext _db;
        public ConsumerAccessLayer(OsirisContext db)
        {
            _db = db;
        }
        /*******************************************************************************************************************/
        // You are free to use any ORM you feel comfortable with for database manipulation
        // You may update the request models and data access interfaces as you wish in order to achieve your end goal
        /******************************************************************************************************************/

        public Customer SaveOrUpdateCustomer(Customer customer)
        {
            _db.Customers.Add(new Data.Customer());
            _db.SaveChanges();
            return customer;
        }

        public Wager SaveOrUpdateWager(Wager wager, bool isValid)
        {
            _db.Wagers.Add(new Data.Wager());
            _db.SaveChanges();
            return wager;
        }

        public void UpdateCustomerLastWager(CustomerLastWager lastWager)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.CustomerId == lastWager.CustomerId);

            if (customer != null)
            {
                customer.LastWagerAmount = lastWager.LastWagerAmount;
                customer.LastWagerDateTime = lastWager.LastWagerDateTime;
                _db.SaveChanges();
            }
        }
    }
}
