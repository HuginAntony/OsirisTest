using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OsirisTest.Hosting;
using OsirisTest.Hosting.DataContracts;
using OsirisTest.Service.Consumer.Consumers;

namespace OsirisTest.Service.Consumer
{
    public class ConsumerBootstrapper: BaseBootstrapper
    {
        public ConsumerBootstrapper(IServiceCollection serviceCollection) : base(serviceCollection, new[] { "appsettings.json"})
        {
        }

        protected override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBaseConsumer, CustomerConsumer>();
            
            services.AddScoped<IBaseConsumer, WagerConsumer>();
        }

        protected override void RegisterLogger(IServiceCollection services)
        {
            services.AddLogging(l => l.AddConsole());
        }
    }
}
