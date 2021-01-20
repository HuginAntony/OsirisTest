using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OsirisTest.Hosting;
using OsirisTest.Hosting.DataContracts;

namespace OsirisTest.Service.Consumer.Consumers.Base
{
    public abstract class BaseConsumer<TMessage> : IBaseConsumer
    {
        private HubConnection _Connection;

        protected readonly ILogger _Logger;
        private readonly string _signalRUrl;


        protected abstract string SignalRChannelName { get; }
        protected abstract string ConsumerName { get; }

        protected BaseConsumer(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _Logger = loggerFactory.CreateLogger(ConsumerName);
            _signalRUrl = configuration.GetValue<string>("url");
        }

        public async Task Register(CancellationToken cancellationToken)
        {
            try
            {
                _Connection = new HubConnectionBuilder()
                    .WithUrl(_signalRUrl)
                    .WithAutomaticReconnect()
                    .Build();

                _Connection.On<TMessage>(SignalRChannelName, MessageHandler);

                await _Connection.StartAsync(cancellationToken);
            }
            catch (OperationCanceledException canEx)
            {
                _Logger.LogWarning($"Consumer {ConsumerName} has been cancelled.\r\n{canEx.Message}", canEx);
            }
            catch (Exception ex)
            {
                _Logger.LogCritical($"Consumer {ConsumerName} failed to register.\r\n{ex.Message}", ex);
                throw;
            }
        }

        private void MessageHandler(TMessage message)
        {
            try
            {
                ProcessMessage(message);

                _Logger.LogInformation($"{ConsumerName} -> {JsonConvert.SerializeObject(message, Formatting.Indented)}\r\n");
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Unexpected error while processing message.\r\nConsumer: {ConsumerName}.\r\n{ex.Message}", ex);
            }
        }

        protected abstract void ProcessMessage(TMessage message);

        public async void DeRegister()
        {
            if (_Connection.State == HubConnectionState.Connected)
            {
                await _Connection.StopAsync();

                await _Connection.DisposeAsync();
            }
        }
    }
}
