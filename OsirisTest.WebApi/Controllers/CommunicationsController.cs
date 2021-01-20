using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OsirisTest.Contracts.RequestModels;
using OsirisTest.WebApi.BusinessLayer.DataContracts;
using OsirisTest.WebApi.Controllers.Base;

namespace OsirisTest.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CommunicationsController: BaseController
    {
        private readonly ICommunicationsManager _CommunicationsManager;
        public CommunicationsController(ILogger<BaseController> logger, ICommunicationsManager communicationsManager) : base(logger)
        {
            _CommunicationsManager = communicationsManager;
        }

        [HttpPost]
        [Route("SendReminderEmail")]
        public async Task<IActionResult> SendReminderEmail([FromBody][Required(ErrorMessage = "Invalid request.")] SendMailRequest request)
        {
            return await RequestHandler(async () => await _CommunicationsManager.SendReminderEmail(request));
        }
    }
}
