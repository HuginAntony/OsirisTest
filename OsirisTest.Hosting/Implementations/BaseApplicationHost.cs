using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OsirisTest.Hosting.DataContracts;

namespace OsirisTest.Hosting.Implementations
{
    public class BaseApplicationHost: IBaseApplicationHost
    {
        private readonly object _Lock = new object();
        private readonly IList<IBaseConsumer> _BaseConsumers;

        public BaseApplicationHost(IEnumerable<IBaseConsumer> consumers)
        {
            _BaseConsumers = consumers.ToList();
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(_BaseConsumers.Select(p => p.Register(cancellationToken)).ToArray());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => Parallel.ForEach(_BaseConsumers, p => p.DeRegister()), cancellationToken);
        }

        public void Dispose()
        {
            lock (_Lock)
            {
                if (_BaseConsumers != null)
                {
                    _BaseConsumers.Clear();

                    _BaseConsumers = null;
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
