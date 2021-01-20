using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OsirisTest.Contracts;
using OsirisTest.Contracts.RequestModels;
using OsirisTest.Service.Consumer.Consumers.Base;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Models;

namespace OsirisTest.Service.Consumer.Consumers
{
    public class CustomerConsumer : BaseConsumer<BaseMessage<Customer>>
    {
        private IConsumerAccessLayer _DateAccessLayer;
        public CustomerConsumer(ILoggerFactory loggerFactory, IConsumerAccessLayer dateAccessLayer, IConfiguration configuration) 
            : base(loggerFactory, configuration)
        {
            _DateAccessLayer = dateAccessLayer;
        }

        protected override string SignalRChannelName => "ReceiveCustomerMessage";

        protected override string ConsumerName => nameof(CustomerConsumer);

        protected override void ProcessMessage(BaseMessage<Customer> message)
        {
            //TODO: Ensure that you do not simultaneously process the same customer

            //TODO: Save or update the customer to database, make sure to only update the latest record by checking the LastUpdateDateTime

            //TODO: Send customer communications using "http://localhost:53395/v1/Communications/SendReminderEmail" if customer's last wager is older than 3 days.
            //CUSTOMER SHOULD NOT RECEIVE MORE THAN ONE MAIL EVERY 24 HOURS
            //**Note that the last wager amount is stored by the WagerConsumer
            //Below is the request model you may use to make the request to the API (http://localhost:53395/v1/Communications/SendReminderEmail)
            SendMailRequest request = new SendMailRequest
            {
                EmailAddress = message.Message.EmailAddress,
                MailSubjectLine = "We have more deals for you",
                MailBody = $"Hi {message.Message.FirstName}.\r\nYour last wager was {message.Message.LastWagerAmount} on {message.Message.LastWagerDateTime}."
            };
        }
    }
}
