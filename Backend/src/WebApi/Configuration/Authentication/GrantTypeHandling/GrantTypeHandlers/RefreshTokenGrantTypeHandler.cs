using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace WebApi.Configuration.Authentication.GrantTypeHandling.GrantTypeHandlers
{
    public class RefreshTokenGrantTypeHandler : IGrantTypeHandler
    {
        public string GrantType => OpenIddictConstants.GrantTypes.RefreshToken;
        
        public async Task<IActionResult> Handle(OpenIddictRequest request, Controller controller)
        {
            var result = await controller.HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                
            var identity = new ClaimsIdentity(authenticationType: TokenValidationParameters.DefaultAuthenticationType);
            identity.SetClaim(OpenIddictConstants.Claims.Subject, result.Principal.GetClaim(OpenIddictConstants.Claims.Subject));
            identity.SetDestinations(c => [OpenIddictConstants.Destinations.AccessToken]);
            identity.SetScopes(request.GetScopes());
                
            return controller.SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}