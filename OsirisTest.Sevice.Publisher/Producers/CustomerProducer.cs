using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OsirisTest.Service.Publisher.Producers.Base;
using OsirisTest.Utilities.DataAccess.Models;
using OsirisTest.Utilities.Extensions;

namespace OsirisTest.Service.Publisher.Producers
{
    public class CustomerProducer: BaseJob<Customer>
    {
        public CustomerProducer(ILoggerFactory loggerFactory, IConfiguration configuration) { }

        protected override string ProducerJobName => nameof(CustomerProducer);

        protected override string SignalRChannelName => "SendCustomerMessage";

        protected override Customer GetMessage()
        {
            int customerId = Random.Next(1, 100);
            return new Customer
            {
                CustomerId = customerId,
                DateOfBirth = DateTime.Now.AddYears(customerId < 18? -customerId : -(18 + customerId)),
                FirstDepositAmount = 10*customerId,
                FirstName = $"Firstname{customerId}",
                LastName = $"LastName{customerId}",
                EmailAddress = $"MailAddress{customerId}@ottest.com",
                LastUpdateDateTime = DateTime.Now.GetRandomDate(Random)
            };
        }
    }
}
