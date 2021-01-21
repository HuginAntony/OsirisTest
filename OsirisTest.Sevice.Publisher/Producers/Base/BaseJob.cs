using System;
using FluentScheduler;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OsirisTest.Contracts;

namespace OsirisTest.Service.Publisher.Producers.Base
{
    public abstract class BaseJob<TMessage> : IJob
    {
        private HubConnection _Connection;

        protected readonly ILogger Logger;
        private readonly string _SignalRUrl;

        protected Random Random { get; }
        
        protected abstract string ProducerJobName { get; }
        protected abstract string SignalRChannelName { get; }


        public BaseJob(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            Logger = loggerFactory.CreateLogger(ProducerJobName);

            _SignalRUrl = configuration.GetValue<string>("SignalRUrl");

            Random = new Random();            

            InitHub();
        }

        private void InitHub()
        {
            try
            {
                _Connection = new HubConnectionBuilder()
                    .WithUrl(_SignalRUrl)
                    .WithAutomaticReconnect()
                    .Build();

                _Connection.StartAsync();

                Logger.LogInformation($"{SignalRChannelName} initialized for {SignalRChannelName}.");
            }
            catch (Exception ex)
            {
                Logger.LogCritical($"Error when connection to R hub for {SignalRChannelName} from {SignalRChannelName}.\r\n{ex.Message}", ex);
                throw;
            }

        }

        public async void Execute()
        {
            try
            {
                TMessage message = GetMessage();

                await _Connection.InvokeAsync<BaseMessage<TMessage>>(SignalRChannelName,
                    new BaseMessage<TMessage>
                    {
                        GeneratedDateTime = DateTime.Now,
                        Message = message
                    });

                Logger.LogInformation($"{SignalRChannelName} -> {JsonConvert.SerializeObject(message, Formatting.Indented)}\r\n");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error when publishing message to {SignalRChannelName} from {ProducerJobName}.\r\n{ex.Message}", ex);
            }
        }

        protected abstract TMessage GetMessage();
    }
}
