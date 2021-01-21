using System;
using System.Linq;
using AutoMapper;
using OsirisTest.Data;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Models;
using Customer = OsirisTest.Utilities.DataAccess.Models.Customer;
using Wager = OsirisTest.Utilities.DataAccess.Models.Wager;

namespace OsirisTest.Utilities.DataAccess.Implementations
{
    public class ConsumerAccessLayer: IConsumerAccessLayer
    {
        private readonly OsirisContext _Db;
        private readonly IMapper _Mapper;

        public ConsumerAccessLayer(OsirisContext db, IMapper mapper)
        {
            _Db = db;
            _Mapper = mapper;
        }
        /*******************************************************************************************************************/
        // You are free to use any ORM you feel comfortable with for database manipulation
        // You may update the request models and data access interfaces as you wish in order to achieve your end goal
        /******************************************************************************************************************/

        public Customer SaveOrUpdateCustomer(Customer customer)
        {
            var currentCustomer = _Db.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);

            if (currentCustomer != null)
            {
                if (customer.LastUpdateDateTime > currentCustomer.LastUpdateDateTime)
                {
                    _Mapper.Map(customer,currentCustomer);

                    if (currentCustomer.LastWagerDateTime == DateTime.MinValue)
                    {
                        currentCustomer.LastWagerDateTime = null;
                    }

                    _Db.Customers.Update(currentCustomer);
                    _Db.SaveChanges();
                }
            }
            else
            {
                currentCustomer = new Data.Customer();

                _Mapper.Map(customer, currentCustomer);

                currentCustomer.InsertedDateTime = DateTime.Now;

                if (currentCustomer.LastWagerDateTime == DateTime.MinValue)
                {
                    currentCustomer.LastWagerDateTime = null;
                }

                _Db.Customers.Add(currentCustomer);
                _Db.SaveChanges();
            }
            return customer;
        }

        public bool IsValidCustomer(int customerId)
        {
            var isValidCustomer = _Db.Customers.Count(c => c.CustomerId == customerId) > 0;

            return isValidCustomer;
        }

        public Wager SaveOrUpdateWager(Wager wager, bool isValid)
        {
            var currentWager = _Db.Wagers.FirstOrDefault(c => c.WagerId == wager.WagerId);

            if (currentWager != null)
            {
                _Mapper.Map(wager, currentWager);
                _Db.Wagers.Update(currentWager);
                    _Db.SaveChanges();
                
            }
            else
            {
                currentWager = new Data.Wager();
                _Mapper.Map(wager, currentWager);
                
                currentWager.InsertedDateTime = DateTime.Now;

                _Db.Wagers.Add(currentWager);
                _Db.SaveChanges();
            }
         
            return wager;
        }

        public void UpdateCustomerLastWager(CustomerLastWager lastWager)
        {
            var customer = _Db.Customers.FirstOrDefault(c => c.CustomerId == lastWager.CustomerId);

            if (customer != null)
            {
                customer.LastWagerAmount = lastWager.LastWagerAmount;
                customer.LastWagerDateTime = lastWager.LastWagerDateTime;
                _Db.SaveChanges();
            }
        }
    }
}
