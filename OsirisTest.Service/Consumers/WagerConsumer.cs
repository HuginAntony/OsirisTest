using System;
using System.Collections.Generic;
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
        private static List<Guid> _ProcessingWagers = new List<Guid>();

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
            if (_ProcessingWagers.Contains(message.Message.WagerId))
            {
                while (_ProcessingWagers.Contains(message.Message.WagerId))
                {
                    //Do nothing until the other thread is completed processing the same Wager
                }
            }

            _ProcessingWagers.Add(message.Message.WagerId);

            var isValidCustomer = await _ConsumerAccessLayer.IsValidCustomer(message.Message.CustomerId);

            if (!isValidCustomer)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/Customer/GetCustomer/{message.Message.CustomerId}");

                var customerResponse = await _HttpClient.Get<CustomerResponse>(request);
                var customer = await _ConsumerAccessLayer.SaveOrUpdateCustomer(_Mapper.Map<Customer>(customerResponse));
            }

            var wager = await _ConsumerAccessLayer.SaveOrUpdateWager(message.Message, message.Message.IsValidWager());

            await UpdateLastWager(message, wager);

            _ProcessingWagers.Remove(message.Message.WagerId);
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
