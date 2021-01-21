using System.Threading.Tasks;
using OsirisTest.Contracts.RequestModels;
using OsirisTest.Contracts.ResponseModels;

namespace OsirisTest.WebApi.BusinessLayer.DataContracts
{
    public interface ICommunicationsManager
    {
        Task<bool> SendReminderEmail(SendMailRequest sendMailRequest);
    }
}
