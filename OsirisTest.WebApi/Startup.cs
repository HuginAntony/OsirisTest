using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OsirisTest.Utilities.DataAccess.DataContracts;
using OsirisTest.Utilities.DataAccess.Implementations;
using OsirisTest.Utilities.SignalRHub;
using OsirisTest.WebApi.BusinessLayer.DataContracts;
using OsirisTest.WebApi.BusinessLayer.Implementations;

namespace OsirisTest.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSignalR();

            services.AddSingleton<ICommunicationsManager, CommunicationsManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IWebDataAccessLayer, WebDataAccessLayer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/MessageHub");
            });
        }
    }
}
