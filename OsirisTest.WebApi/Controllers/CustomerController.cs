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
        [Route("IsCustomerLocked/{customerId}")]
        public async Task<IActionResult> IsCustomerLocked(int customerId)
        {
            var response = await _CustomerManager.IsLockedCustomer(customerId);
            return await RequestHandler(() => response);
        }

        [HttpGet]
        [Route("GetCustomer/{customerId}")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            var response = await _CustomerManager.GetCustomerById(customerId);
            return await RequestHandler(() => response);
        } 
    }
}
