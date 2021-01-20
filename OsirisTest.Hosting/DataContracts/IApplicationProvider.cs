using Microsoft.Extensions.DependencyInjection;

namespace OsirisTest.Hosting.DataContracts
{
    public interface IApplicationProvider
    {
        public void RunApplication() { };
    }
}
