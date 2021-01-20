using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsirisTest.Hosting;
using OsirisTest.Hosting.DataContracts;
using OsirisTest.Service.Publisher.Producers;
using OsirisTest.Service.Publisher.Producers.Base;

namespace OsirisTest.Service.Publisher
{
    public class ProducerBootstrapper: BaseBootstrapper
    {
        public ProducerBootstrapper(IServiceCollection serviceCollection) : base(serviceCollection, new[] { "appsettings.json"})
        {

        }

        protected override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<WagerProducer>();

            services.AddScoped<IBaseConsumer, BaseProducer<WagerProducer>>();
        }

        protected override void RegisterLogger(IServiceCollection services)
        {
        }
    }
}
