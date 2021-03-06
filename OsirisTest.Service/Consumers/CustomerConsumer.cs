﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OsirisTest.Contracts;
using OsirisTest.Contracts.RequestModels;
using OsirisTest.Service.Consumer.Consumers.Base;
using OsirisTest.Service.Consumer.Contracts;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Models;

namespace OsirisTest.Service.Consumer.Consumers
{
    public class CustomerConsumer : BaseConsumer<BaseMessage<Customer>>
    {
        private readonly IConsumerAccessLayer _ConsumerAccessLayer;
        private readonly IHttpClient _HttpClient;
        private static List<int> _ProcessingCustomers = new List<int>();

        public CustomerConsumer(ILoggerFactory loggerFactory, IConsumerAccessLayer consumerAccessLayer, IConfiguration configuration, IHttpClient httpClient) 
            : base(loggerFactory, configuration)
        {
            _ConsumerAccessLayer = consumerAccessLayer;
            _HttpClient = httpClient;
        }

        protected override string SignalRChannelName => "ReceiveCustomerMessage";

        protected override string ConsumerName => nameof(CustomerConsumer);

        protected override async void ProcessMessage(BaseMessage<Customer> message)
        {
            if (_ProcessingCustomers.Contains(message.Message.CustomerId))
            {
                while (_ProcessingCustomers.Contains(message.Message.CustomerId))
                {
                    //Do nothing until the other thread is completed processing the same customer
                }
            }

            _ProcessingCustomers.Add(message.Message.CustomerId);
            var customer = await _ConsumerAccessLayer.SaveOrUpdateCustomer(message.Message);

            await SendEmail(message, customer);

            _ProcessingCustomers.Remove(message.Message.CustomerId);
        }

        private async Task SendEmail(BaseMessage<Customer> message, Customer customer)
        {
            //CUSTOMER SHOULD NOT RECEIVE MORE THAN ONE MAIL EVERY 24 HOURS
            var lastEmail = await _ConsumerAccessLayer.GetLastEmailDate(customer.CustomerId);
            var time = DateTime.Now - lastEmail;

            if (time.TotalHours > 24)
            {
                _ConsumerAccessLayer.UpdateLastEmailDate(customer.CustomerId);

                if (customer.LastWagerDateTime != DateTime.MinValue && DateTime.Now > customer.LastWagerDateTime.AddDays(-3))
                {
                    SendMailRequest body = new SendMailRequest
                    {
                        EmailAddress = message.Message.EmailAddress,
                        MailSubjectLine = "We have more deals for you",
                        MailBody =
                            $"Hi {message.Message.FirstName}.\r\nYour last wager was {message.Message.LastWagerAmount} on {message.Message.LastWagerDateTime}."
                    };

                    var request = new HttpRequestMessage(HttpMethod.Post, "/v1/Communications/SendReminderEmail");

                    request.Content = new StringContent(JsonSerializer.Serialize(body));
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await _HttpClient.Post<string>(request);
                }
            }
        }
    }
}
