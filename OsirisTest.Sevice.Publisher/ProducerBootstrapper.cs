using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

        protected override void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("DBConnection");

            //services.AddDbContext<OsirisContext>(options =>
            //    options.UseSqlServer(connectionString));
        }

        protected override void RegisterLogger(IServiceCollection services)
        {
            services.AddLogging(l => l.AddConsole());
        }
    }
}
