﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OsirisTest.Contracts;
using OsirisTest.Service.Consumer.Consumers.Base;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Models;

namespace OsirisTest.Service.Consumer.Consumers
{
    public class WagerConsumer : BaseConsumer<BaseMessage<Wager>>
    {
        private IConsumerAccessLayer _DateAccessLayer;
        public WagerConsumer(ILoggerFactory loggerFactory, IConsumerAccessLayer dateAccessLayer, IConfiguration configuration) 
            : base(loggerFactory, configuration)
        {
            _DateAccessLayer = dateAccessLayer;
        }

        protected override string SignalRChannelName => "ReceiveWagerMessage";

        protected override string ConsumerName => nameof(WagerConsumer);

        protected override void ProcessMessage(BaseMessage<Wager> message)
        {
            //TODO: Ensure that you do not simultaneously process the same wager (This should not be the case with the WagerId being a random Guid but do cater for it either way)

            //TODO: Save or update the wager to database, use the provided extension to get the Validity of the wager (ContractExtensions.IsValidWager)

            //TODO: If customer does not exist on the local data store then request customer from 3rd party API and update the customer record locally (http://localhost:53395/v1/Customer/GetCustomer)

            //TODO: If the wager IsValid and the Customer is NOT LOCKED ("http://localhost:53395/v1/Customer/IsCustomerLocked"), update the customer's LastWagerAmount and LastWagerDateTime.  Be sure to update this value for the latest valid wager only.
        }
    }
}