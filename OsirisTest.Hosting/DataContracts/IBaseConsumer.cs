using System.Threading;
using System.Threading.Tasks;

namespace OsirisTest.Hosting.DataContracts
{
    public  interface IBaseConsumer
    {
        public Task Register(CancellationToken cancellationToken) { };

        public void DeRegister() { };
    }
}
