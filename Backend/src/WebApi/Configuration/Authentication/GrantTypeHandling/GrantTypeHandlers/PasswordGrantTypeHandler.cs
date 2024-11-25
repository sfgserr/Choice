using System.Security.Claims;
using Identity.Application.Authentication.Authenticate;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace WebApi.Configuration.Authentication.GrantTypeHandling.GrantTypeHandlers
{
    public class PasswordGrantTypeHandler : IGrantTypeHandler
    {
        private readonly IIdentityModule _identityModule;

        public PasswordGrantTypeHandler(IIdentityModule identityModule)
        {
            _identityModule = identityModule;
        }

        public string GrantType => OpenIddictConstants.GrantTypes.Password;
        
        public async Task<IActionResult> Handle(OpenIddictRequest request, Controller controller)
        {
            var result = await _identityModule.ExecuteCommand<AuthenticateCommand, AuthenticationResult>(
                new AuthenticateCommand(
                    request.Username,
                    request.Password));

            if (!result.IsSuccessful)
                return controller.Unauthorized();
                    
            var identity = new ClaimsIdentity(authenticationType: TokenValidationParameters.DefaultAuthenticationType);
                
            identity.SetClaim(OpenIddictConstants.Claims.Subject, result.UserId!.ToString());
            identity.SetDestinations(c => [OpenIddictConstants.Destinations.AccessToken]);
            identity.SetScopes(request.GetScopes());
                
            return controller.SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}