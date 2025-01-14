using System.Security.Claims;
using Identity.Application.Authorization.GetUser;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Payments.Application.Contracts;
using Payments.Application.Subscriptions.Queries.CheckActiveSubscription;

namespace WebApi.Configuration.Authentication.GrantTypeHandling.GrantTypeHandlers
{
    public class RefreshTokenGrantTypeHandler : IGrantTypeHandler
    {
        private readonly IIdentityModule _identityModule;
        private readonly IPaymentsModule _paymentsModule;
        
        public RefreshTokenGrantTypeHandler(IIdentityModule identityModule, IPaymentsModule paymentsModule)
        {
            _identityModule = identityModule;
            _paymentsModule = paymentsModule;
        }

        public string GrantType => OpenIddictConstants.GrantTypes.RefreshToken;
        
        public async Task<IActionResult> Handle(OpenIddictRequest request, Controller controller)
        {
            var result = await controller.HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            string id = result.Principal.GetClaim(OpenIddictConstants.Claims.Subject);
            
            var user = await _identityModule.Query<GetUserQuery, UserDto>(
                new GetUserQuery(Guid.Parse(id)));
            
            var identity = new ClaimsIdentity(authenticationType: TokenValidationParameters.DefaultAuthenticationType);
            
            if (user.RoleCode == "Company")
            {
                var subscribed = await _paymentsModule.Query<CheckActiveSubscriptionQuery, bool>(
                    new CheckActiveSubscriptionQuery(Guid.Parse(id)));
                
                identity.SetClaim("subscribed", subscribed);
            }
            
            identity.SetClaim(OpenIddictConstants.Claims.Subject, id);
            identity.SetClaim("type", user.RoleCode);
            identity.SetDestinations(c => [OpenIddictConstants.Destinations.AccessToken]);
            identity.SetScopes(request.GetScopes());
                
            return controller.SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}