using Identity.Application.Authentication.Phone.GetCode;
using Identity.Application.Contracts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Configuration.Authentication.GrantTypeHandling;

namespace WebApi.Modules.Identity
{
    [Route("api/auth")]
    public class IdentityController : Controller
    {
        private readonly GrantTypeHandlerFactory _factory;
        private readonly IIdentityModule _module;
        
        public IdentityController(GrantTypeHandlerFactory factory, IIdentityModule module)
        {
            _factory = factory;
            _module = module;
        }
        
        [HttpPost("token")]
        public async Task<IActionResult> Token()
        {
            var request = HttpContext.GetOpenIddictServerRequest();

            if (request is null)
                return BadRequest();

            var handler = _factory.GetHandler(request);

            return await handler.Handle(request, this);
        }

        [HttpPost("code")]
        public async Task<IActionResult> GetCode(string phoneNumber)
        {
            await _module.ExecuteCommand(new GetCodeCommand(phoneNumber));
            
            return Ok();
        }
    }
}