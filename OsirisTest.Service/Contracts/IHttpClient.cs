using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OsirisTest.Service.Consumer.Contracts
{
    public interface IHttpClient
    {
        Task<T> Get<T>(HttpRequestMessage requestMessage);
        Task<T> Post<T>(HttpRequestMessage requestMessage);
    }
}
