using System.Threading;
using System.Threading.Tasks;
using OsirisTest.Hosting.DataContracts;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.ServiceConfigurators;

namespace OsirisTest.Hosting.Implementations
{
    public abstract class TopShelfApplicationProvider<THost> : IApplicationProvider where THost : class, IBaseApplicationHost
    {
        private readonly THost _ApplicationHost;

        private readonly CancellationTokenSource _CancellationTokenSource;

        protected abstract string ServiceName { get; }

        protected abstract string ServiceDisplayName { get; }

        protected abstract string ServiceDescription { get; }

        protected TopShelfApplicationProvider(THost eventHost)
        {
            _ApplicationHost = eventHost;
            _CancellationTokenSource = new CancellationTokenSource();
        }

        private void ConfigureHost(HostConfigurator hostConfigurator)
        {
            hostConfigurator.Service<THost>(ConfigureService);

            hostConfigurator.SetDisplayName(ServiceName);
            hostConfigurator.SetServiceName(ServiceDisplayName);
            hostConfigurator.SetDescription(ServiceDescription);

            hostConfigurator.RunAsLocalService();
        }

        private void ConfigureService(ServiceConfigurator<THost> serviceConfigurator)
        {
            serviceConfigurator.ConstructUsing(ConstructService);
            serviceConfigurator.WhenStarted(WhenStarted);
            serviceConfigurator.WhenStopped(WhenStopped);
        }

        private THost ConstructService()
        {
            return _ApplicationHost;
        }

        private void WhenStarted(THost eventHost)
        {
            Task.Run(async () => await eventHost.StartAsync(_CancellationTokenSource.Token));
        }

        private void WhenStopped(THost eventHost)
        {
            Task.Run(async () =>
            {
                _CancellationTokenSource.Cancel();
                await eventHost.StopAsync(_CancellationTokenSource.Token);
                eventHost.Dispose();
            });
        }

        public void RunApplication()
        {
            Host topShelfHost = HostFactory.New(ConfigureHost);
            topShelfHost.Run();
        }
    }
}
