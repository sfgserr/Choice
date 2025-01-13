using System.Security.Claims;
using Identity.Application.Authentication.Authenticate;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Payments.Application.Contracts;
using Payments.Application.Subscriptions.Queries.CheckActiveSubscription;

namespace WebApi.Configuration.Authentication.GrantTypeHandling.GrantTypeHandlers
{
    public class PasswordGrantTypeHandler : IGrantTypeHandler
    {
        private readonly IIdentityModule _identityModule;
        private readonly IPaymentsModule _paymentsModule;
        
        public PasswordGrantTypeHandler(IIdentityModule identityModule, IPaymentsModule paymentsModule)
        {
            _identityModule = identityModule;
            _paymentsModule = paymentsModule;
        }

        public string GrantType => OpenIddictConstants.GrantTypes.Password;
        
        public async Task<IActionResult> Handle(OpenIddictRequest request, Controller controller)
        {
            var result = await _identityModule.ExecuteCommand<AuthenticateCommand, AuthenticationResult>(
                new AuthenticateCommand(
                    request.Username,
                    request.Password));

            if (!result.IsSuccessful)
                return controller.Unauthorized(result.ErrorMessage);
            
            var identity = new ClaimsIdentity(authenticationType: TokenValidationParameters.DefaultAuthenticationType);
                
            identity.SetClaim(OpenIddictConstants.Claims.Subject, result.User.UserId.ToString());
            identity.SetClaim("type", result.User.UserType);

            if (result.User.UserType == "Company")
            {
                var subscribed = await _paymentsModule.Query<CheckActiveSubscriptionQuery, bool>(
                    new CheckActiveSubscriptionQuery(result.User.UserId));
                
                identity.SetClaim("subscribed", subscribed);
            }
            
            identity.SetDestinations(c => [OpenIddictConstants.Destinations.AccessToken]);
            identity.SetScopes(request.GetScopes());
                
            return controller.SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}