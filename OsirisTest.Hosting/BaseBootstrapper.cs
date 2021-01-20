using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsirisTest.Hosting.DataContracts;
using OsirisTest.Hosting.Implementations;

namespace OsirisTest.Hosting
{
    public abstract class BaseBootstrapper : IDisposable
    {
        private bool _DisposedValue;

        private IServiceCollection _ServiceCollection;
        private IConfiguration _Configuration;

        protected BaseBootstrapper(IServiceCollection serviceCollection, string[] jsonConfigFileNames)
        {
            _ServiceCollection = serviceCollection;
            LoadJsonConfigFiles(jsonConfigFileNames);
        }

        private void Register()
        {
            ConfigureServices(_ServiceCollection, _Configuration);

            RegisterLogger(_ServiceCollection);

            RegisterInternal();
        }

        protected abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        protected abstract void RegisterLogger(IServiceCollection services);

        private void RegisterInternal()
        {
            _ServiceCollection.AddSingleton<IApplicationProvider, TopShelfConsumerApplicationProvider>();

            _ServiceCollection.AddSingleton<IBaseApplicationHost, BaseApplicationHost>();

            _ServiceCollection.AddSingleton(_Configuration);

            _ServiceCollection.AddSingleton(_ServiceCollection.BuildServiceProvider());
        }

        private void LoadJsonConfigFiles(string[] jsonConfigFiles)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            foreach (string configFile in jsonConfigFiles)
            {
                configurationBuilder.AddJsonFile(configFile);
            }

            _Configuration = configurationBuilder.Build();
        }

        public void RunTopShelfService()
        {
            Register();

            IApplicationProvider applicationProvider = _ServiceCollection.BuildServiceProvider().GetService<IApplicationProvider>();

            applicationProvider.RunApplication();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_DisposedValue)
                return;

            if (disposing)
            {
                _ServiceCollection?.Clear();
            }


            _ServiceCollection = null;
            _DisposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize((object)this);
        }
    }


}
