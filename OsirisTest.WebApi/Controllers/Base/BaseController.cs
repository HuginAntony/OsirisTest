using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OsirisTest.WebApi.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _Logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _Logger = logger;
        }

        protected async Task<IActionResult> RequestHandler<T>(Func<T> func, string modelStateErrorMessage = "Invalid request model.")
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(modelStateErrorMessage);
                }

                T response = await func();

                if (response == null)
                {
                    //Ignore the fact that there are no custom error messages
                    return BadRequest($"{func.Method.Name} -> Unknown error.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"{func.Method.Name} -> Unexpected error.", ex);

                //Ignore the fact that there are no custom error messages
                return BadRequest($"{func.Method.Name} -> Unexpected error.");
            }
        }
    }
}
