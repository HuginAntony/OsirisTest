using Microsoft.Extensions.DependencyInjection;

namespace OsirisTest.Service.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ConsumerBootstrapper bootstrapper = new ConsumerBootstrapper(new ServiceCollection()))
            {
                bootstrapper.RunTopShelfService();
            }
        }
    }
}
