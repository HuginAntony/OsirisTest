using System.Threading;
using System.Threading.Tasks;

namespace OsirisTest.Hosting.DataContracts
{
    public  interface IBaseConsumer
    {
        Task Register(CancellationToken cancellationToken);

        void DeRegister();
    }
}
