using System.Security.Claims;
using Identity.Application.Authentication.Authenticate;
using Identity.Application.Contracts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace WebApi.Modules.Identity
{
    [Route("api/auth")]
    public class IdentityController : Controller
    {
        private readonly IIdentityModule _identityModule;
        
        public IdentityController(IIdentityModule identityModule)
        {
            _identityModule = identityModule;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            var request = HttpContext.GetOpenIddictServerRequest();

            if (request is not null && request.IsPasswordGrantType())
            {
                var result = await _identityModule.ExecuteCommand<AuthenticateCommand, AuthenticationResult>(
                    new AuthenticateCommand(
                        request.Username,
                        request.Password));

                if (!result.IsSuccessful)
                    return Unauthorized();
                    
                var identity = new ClaimsIdentity(authenticationType: TokenValidationParameters.DefaultAuthenticationType);
                
                identity.SetClaim(OpenIddictConstants.Claims.Subject, result.UserId!.ToString());
                identity.SetDestinations(c => [OpenIddictConstants.Destinations.AccessToken]);
                identity.SetScopes(request.GetScopes());
                
                return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new NotImplementedException("Other grant types are not implemented");
        }
    }
}