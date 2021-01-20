using Microsoft.Extensions.DependencyInjection;

namespace OsirisTest.Service.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ProducerBootstrapper bootstrapper = new ProducerBootstrapper(new ServiceCollection()))
            {
                bootstrapper.RunTopShelfService();
            }
        }
    }
}
