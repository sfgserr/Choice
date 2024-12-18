using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Configuration.Authentication.GrantTypeHandling;

namespace WebApi.Modules.Identity
{
    [Route("api/auth")]
    public class IdentityController : Controller
    {
        private readonly GrantTypeHandlerFactory _factory;
        
        public IdentityController(GrantTypeHandlerFactory factory)
        {
            _factory = factory;
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
    }
}