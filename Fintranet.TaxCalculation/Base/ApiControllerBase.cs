using Fintranet.TaxCalculation.Api.Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fintranet.TaxCalculation.Api.Base.Models;
using System.Security.Claims;

namespace Fintranet.TaxCalculation.Api.Base
{
    [ApiController]
    [Authorize]
    public class ApiControllerBase : ControllerBase
    {
        protected string AccessToken => Request.GetAccessToken();
        protected virtual string UserName => ClaimHelper.GetClaim<string>(AccessToken, ClaimTypes.GivenName);
        protected virtual Guid UserId => ClaimHelper.GetClaim<Guid>(AccessToken, "id");
        protected virtual string Name => ClaimHelper.GetClaim<string>(AccessToken, ClaimTypes.Name);

        protected ApiControllerBase()
        {
        }
        #region responses 
        [NonAction]
        public BadRequestObjectResult BadRequest(string message)
        {
            return BadRequest(new { Message = message });
        }

        [NonAction]
        public NotFoundObjectResult NotFound(string message)
        {
            return NotFound(new { Message = message });
        }

        [NonAction]
        protected virtual IActionResult OkResult()
        {
            return OkResult("", null);
        }

        [NonAction]
        protected virtual IActionResult OkResult(string message)
        {
            return OkResult(message, null);
        }

        [NonAction]
        protected virtual IActionResult OkContentResult(object content)
        {
            return OkResult("", content);
        }

        [NonAction]
        protected virtual IActionResult OkResult(string message, object content)
        {
            return Ok(new ResponseMessage(message, content));
        }

        [NonAction]
        protected virtual IActionResult OkResult(string message, object content, int total)
        {
            return Ok(new ResponseMessage(message, content, total));
        }
        #endregion

        public class RequestConfig
        {
            public static RequestConfig Default => new RequestConfig();

            /// <inheritdoc />
            public RequestConfig(bool returnContentOnly = false)
            {
                ReturnContentOnly = returnContentOnly;
            }

            public bool ReturnContentOnly { get; set; }

            public bool ReturnActionOutputOnly { get; set; }
        }
    }
}
