using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OsirisTest.Data;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Models;
using Customer = OsirisTest.Utilities.DataAccess.Models.Customer;
using Wager = OsirisTest.Utilities.DataAccess.Models.Wager;

namespace OsirisTest.Utilities.DataAccess.Implementations
{
    public class ConsumerAccessLayer : IConsumerAccessLayer
    {
        private readonly IMapper _Mapper;

        public ConsumerAccessLayer(IMapper mapper)
        {
            _Mapper = mapper;
        }

        public async Task<Customer> SaveOrUpdateCustomer(Customer customer)
        {
            var db = new OsirisContext();

            var currentCustomer = await db.Customers.FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            
            if (currentCustomer != null)
            {
                if (customer.LastUpdateDateTime > currentCustomer.LastUpdateDateTime)
                {
                    _Mapper.Map(customer, currentCustomer);

                    if (currentCustomer.LastWagerDateTime == DateTime.MinValue)
                    {
                        currentCustomer.LastWagerDateTime = null;
                    }

                    db.Customers.Update(currentCustomer);
                    await db.SaveChangesAsync();
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

                await db.Customers.AddAsync(currentCustomer);
                await db.SaveChangesAsync();
            }
            await db.DisposeAsync();

            return customer;
        }

        public async Task<bool> IsValidCustomer(int customerId)
        {
            var db = new OsirisContext();

            var isValidCustomer = await db.Customers.CountAsync(c => c.CustomerId == customerId) > 0;

            await db.DisposeAsync();

            return isValidCustomer;
        }

        public async Task<Wager> SaveOrUpdateWager(Wager wager, bool isValid)
        {
            var db = new OsirisContext();

            var currentWager = await db.Wagers.FirstOrDefaultAsync(c => c.WagerId == wager.WagerId);

            if (currentWager != null)
            {
                _Mapper.Map(wager, currentWager);
                currentWager.IsValid = isValid;

                db.Wagers.Update(currentWager);
                await db.SaveChangesAsync();
            }
            else
            {
                currentWager = new Data.Wager();
                _Mapper.Map(wager, currentWager);

                currentWager.IsValid = isValid;
                currentWager.InsertedDateTime = DateTime.Now;

                await db.Wagers.AddAsync(currentWager);
                await db.SaveChangesAsync();
            }
            
            await db.DisposeAsync();

            return wager;
        }

        public async void UpdateCustomerLastWager(CustomerLastWager lastWager)
        {
            var db = new OsirisContext();

            var customer = await db.Customers.FirstOrDefaultAsync(c => c.CustomerId == lastWager.CustomerId);

            if (customer != null)
            {
                customer.LastWagerAmount = lastWager.LastWagerAmount;
                customer.LastWagerDateTime = lastWager.LastWagerDateTime;
                await db.SaveChangesAsync();
            }
            await db.DisposeAsync();
        }
    }
}
