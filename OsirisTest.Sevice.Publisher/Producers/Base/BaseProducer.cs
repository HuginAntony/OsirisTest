using System;
using System.Threading;
using System.Threading.Tasks;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OsirisTest.Hosting.DataContracts;

namespace OsirisTest.Service.Publisher.Producers.Base
{
    public class BaseProducer<TJob> : IBaseConsumer where TJob : IJob
    {
        private readonly string _JobName;
        private readonly ILogger _Logger;
        private readonly ServiceProvider _ServiceProvider;

        public BaseProducer(ILoggerFactory loggerFactory, ServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
            _JobName = nameof(TJob);
            _Logger = loggerFactory.CreateLogger(_JobName);
        }

        public Task Register(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                try
                {
                    //IGNORE Scheduling initializer and interval section
                    Registry registry = new Registry();

                    if (typeof(TJob) == typeof(CustomerProducer))
                    {
                        registry.Schedule(_ServiceProvider.GetService<TJob>()).ToRunNow().AndEvery(2).Seconds();
                    }
                    else
                    {
                        //Will only run in 30 seconds after a couple of customer have been added
                        registry.Schedule(_ServiceProvider.GetService<TJob>()).ToRunOnceAt(DateTime.Now.AddSeconds(30)).AndEvery(2).Milliseconds();
                    }
                    
                    JobManager.Initialize(registry);

                    JobManager.Start();

                    _Logger.LogInformation($"Producer {_JobName} to registered successfully.");
                }
                catch (Exception ex)
                {
                    _Logger.LogCritical($"Producer {_JobName} failed to register.\r\n{ex.Message}", ex);
                    throw;
                }
            }, cancellationToken);
        }

        public void DeRegister()
        {
            await Task.Run(JobManager.StopAndBlock);
        }
    }
}
