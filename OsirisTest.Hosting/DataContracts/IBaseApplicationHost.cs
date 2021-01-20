using System;
using Microsoft.Extensions.Hosting;

namespace OsirisTest.Hosting.DataContracts
{
    public interface IBaseApplicationHost : IHostedService, IDisposable
    {
    }
}
