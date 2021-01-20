using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OsirisTest.Service.Publisher.Producers.Base;
using OsirisTest.Utilities.DataAccess.Models;
using OsirisTest.Utilities.Extensions;

namespace OsirisTest.Service.Publisher.Producers
{
    public class WagerProducer: BaseJob<Wager>
    {
        public WagerProducer(ILoggerFactory loggerFactory, IConfiguration configuration) { }

        protected override string ProducerJobName => nameof(WagerProducer);

        protected override string SignalRChannelName => "SendWagerMessage";

        protected override Wager GetMessage()
        {
            return new Wager
            {
                CustomerId = Random.Next(1, 100),
                Amount = 1.5m * Random.Next(0, 10),
                WagerDateTime = DateTime.Now.GetRandomDate(Random),
                WagerId = Guid.NewGuid()
            };
        }
    }
}
