using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OsirisTest.WebApi.BusinessLayer.DataContracts;
using OsirisTest.WebApi.Controllers.Base;

namespace OsirisTest.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CustomerController: BaseController
    {
        private readonly ICustomerManager _CustomerManager;
        public CustomerController(ILogger<BaseController> logger, ICustomerManager customerManager) : base(logger)
        {
            _CustomerManager = customerManager;
        }

        [HttpGet]
        [Route("IsCustomerLocked")]
        public Task<IActionResult> IsCustomerLocked(int customerId)
        {
            return await RequestHandler(() => await _CustomerManager.IsLockedCustomer(customerId));
        }

        [HttpGet]
        [Route("GetCustomer")]
        public Task<IActionResult> GetCustomer(int customerId)
        {
            return await RequestHandler(() => await _CustomerManager.GetCustomerById(customerId));
        }
    }
}
