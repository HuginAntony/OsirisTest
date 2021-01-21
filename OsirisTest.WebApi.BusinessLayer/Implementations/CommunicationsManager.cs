using System.Threading.Tasks;
using OsirisTest.Contracts.RequestModels;
using OsirisTest.Contracts.ResponseModels;
using OsirisTest.WebApi.BusinessLayer.DataContracts;

namespace OsirisTest.WebApi.BusinessLayer.Implementations
{
    public class CommunicationsManager : ICommunicationsManager
    {
        public Task<bool> SendReminderEmail(SendMailRequest sendMailRequest)
        {
            //DO NOTHING
            return Task.FromResult(true);
        }
    }
}
