using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        private override void RegisterLogger(IServiceCollection services)
        {
            
        }
    }
}
