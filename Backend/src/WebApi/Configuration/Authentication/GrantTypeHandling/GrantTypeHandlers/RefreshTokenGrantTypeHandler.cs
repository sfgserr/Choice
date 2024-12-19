using System.Security.Claims;
using Identity.Application.Authorization.GetUser;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace WebApi.Configuration.Authentication.GrantTypeHandling.GrantTypeHandlers
{
    public class RefreshTokenGrantTypeHandler : IGrantTypeHandler
    {
        private readonly IIdentityModule _identityModule;

        public RefreshTokenGrantTypeHandler(IIdentityModule identityModule)
        {
            _identityModule = identityModule;
        }

        public string GrantType => OpenIddictConstants.GrantTypes.RefreshToken;
        
        public async Task<IActionResult> Handle(OpenIddictRequest request, Controller controller)
        {
            var result = await controller.HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            string id = result.Principal.GetClaim(OpenIddictConstants.Claims.Subject);
            
            var user = await _identityModule.Query<GetUserQuery, UserDto>(
                new GetUserQuery(Guid.Parse(id)));
            
            var identity = new ClaimsIdentity(authenticationType: TokenValidationParameters.DefaultAuthenticationType);
            identity.SetClaim(OpenIddictConstants.Claims.Subject, id);
            identity.SetClaim("type", user.RoleCode);
            identity.SetDestinations(c => [OpenIddictConstants.Destinations.AccessToken]);
            identity.SetScopes(request.GetScopes());
                
            return controller.SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}