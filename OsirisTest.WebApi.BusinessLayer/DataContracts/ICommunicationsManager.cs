using System.Threading.Tasks;
using OsirisTest.Contracts.RequestModels;
using OsirisTest.Contracts.ResponseModels;

namespace OsirisTest.WebApi.BusinessLayer.DataContracts
{
    public interface ICommunicationsManager
    {
        Task<Response> SendReminderEmail(SendMailRequest sendMailRequest);
    }
}
