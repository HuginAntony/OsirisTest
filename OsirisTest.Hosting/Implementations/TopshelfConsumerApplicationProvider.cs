using System;
using System.Reflection;
using OsirisTest.Hosting.DataContracts;

namespace OsirisTest.Hosting.Implementations
{
    public class TopShelfConsumerApplicationProvider : TopShelfApplicationProvider<IBaseApplicationHost>
    {
        private readonly Assembly _ExecutingAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
        private readonly string _AssemblyName;


        public TopShelfConsumerApplicationProvider(IBaseApplicationHost eventHost) : base(eventHost)
        {
            _AssemblyName = _ExecutingAssembly.GetName().Name;
        }

        protected override string ServiceName => GetServiceName();

        protected override string ServiceDisplayName => GetServiceDisplayName();

        protected override string ServiceDescription => GetServiceDescription();


        private string GetServiceName()
        {
            return _AssemblyName.StartsWith("OsirisTest", StringComparison.InvariantCultureIgnoreCase) ? $".{_AssemblyName}" : $".OsirisTest{_AssemblyName}";
        }

        private string GetServiceDisplayName()
        {
            return _AssemblyName.StartsWith("OsirisTest", StringComparison.InvariantCultureIgnoreCase) ? $".{_AssemblyName}" : $".OsirisTest{_AssemblyName}";
        }

        private string GetServiceDescription()
        {
            return _AssemblyName.StartsWith("OsirisTest", StringComparison.InvariantCultureIgnoreCase) ? $".{_AssemblyName}" : $".OsirisTest{_AssemblyName}";
        }
    }
}
