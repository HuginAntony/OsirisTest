using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OsirisTest.Data;
using OsirisTest.Hosting;
using OsirisTest.Hosting.DataContracts;
using OsirisTest.Service.Consumer.Consumers;
using OsirisTest.Service.Consumer.Contracts;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Implementations;

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
            services.AddScoped<IConsumerAccessLayer, ConsumerAccessLayer>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IHttpClient, RestClient>(i => new RestClient("http://localhost:53395"));
        }

        protected override void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DBConnection");

            services.AddDbContext<OsirisContext>(options =>
                options.UseSqlServer(connectionString));
        }

        protected override void RegisterLogger(IServiceCollection services)
        {
            services.AddLogging(l => l.AddConsole());
        }
    }
}
