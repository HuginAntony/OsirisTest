using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OsirisTest.Contracts;
using OsirisTest.Contracts.ResponseModels;
using OsirisTest.Service.Consumer.Consumers.Base;
using OsirisTest.Service.Consumer.Contracts;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Models;
using OsirisTest.Utilities.Extensions;

namespace OsirisTest.Service.Consumer.Consumers
{
    public class WagerConsumer : BaseConsumer<BaseMessage<Wager>>
    {
        private readonly IConsumerAccessLayer _ConsumerAccessLayer;
        private readonly IHttpClient _HttpClient;
        private readonly IMapper _Mapper;

        public WagerConsumer(ILoggerFactory loggerFactory, IConsumerAccessLayer consumerAccessLayer, IConfiguration configuration, 
            IHttpClient httpClient, IMapper mapper) 
            : base(loggerFactory, configuration)
        {
            _ConsumerAccessLayer = consumerAccessLayer;
            _HttpClient = httpClient;
            _Mapper = mapper;
        }

        protected override string SignalRChannelName => "ReceiveWagerMessage";

        protected override string ConsumerName => nameof(WagerConsumer);

        protected override async void ProcessMessage(BaseMessage<Wager> message)
        {
            //TODO: Ensure that you do not simultaneously process the same wager (This should not be the case with the WagerId being a random Guid but do cater for it either way)

            var isValidCustomer = await _ConsumerAccessLayer.IsValidCustomer(message.Message.CustomerId);

            if (!isValidCustomer)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/Customer/GetCustomer/{message.Message.CustomerId}");

                var customerResponse = await _HttpClient.Get<CustomerResponse>(request);
                var customer = await _ConsumerAccessLayer.SaveOrUpdateCustomer(_Mapper.Map<Customer>(customerResponse));
            }

            var wager = await _ConsumerAccessLayer.SaveOrUpdateWager(message.Message, message.Message.IsValidWager());

            await UpdateLastWager(message, wager);
        }

        private async Task UpdateLastWager(BaseMessage<Wager> message, Wager wager)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/Customer/IsCustomerLocked/{wager.CustomerId}");

            var isCustomerLocked = await _HttpClient.Get<bool>(request);

            if (message.Message.IsValidWager() && !isCustomerLocked)
            {
                var lastWager = new CustomerLastWager
                {
                    CustomerId = wager.CustomerId,
                    LastWagerAmount = wager.Amount,
                    LastWagerDateTime = wager.WagerDateTime
                };

                _ConsumerAccessLayer.UpdateCustomerLastWager(lastWager);
            }
        }
    }
}
