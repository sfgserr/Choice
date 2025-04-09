using System.Security.Claims;
using Identity.Application.Authentication;
using Identity.Application.Authentication.Phone.VerifyCode;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace WebApi.Configuration.Authentication.GrantTypeHandling.GrantTypeHandlers
{
    public class PasswordPhoneGrantTypeHandler : IGrantTypeHandler
    {
        private readonly IIdentityModule _module;

        public PasswordPhoneGrantTypeHandler(IIdentityModule module)
        {
            _module = module;
        }

        public string GrantType => "password_phone";

        public async Task<IActionResult> Handle(OpenIddictRequest request, Controller controller)
        {
            var code = request.GetParameter("code").ToString();

            if (code == null)
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "code paramenter is not found"
                });
                
                return controller.Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var result = await _module.ExecuteCommand<VerifyCodeCommand, AuthenticationResult>(new(code));
            
            if (!result.IsSuccessful)
                return controller.Unauthorized(result.ErrorMessage);
            
            var identity = new ClaimsIdentity(authenticationType: TokenValidationParameters.DefaultAuthenticationType);
                
            identity.SetClaim(OpenIddictConstants.Claims.Subject, result.User.UserId.ToString());
            identity.SetClaim("type", result.User.UserType);

            identity.SetClaim("subscribed", result.User.IsSubscribed);
            identity.SetDestinations(c => [OpenIddictConstants.Destinations.AccessToken]);
            identity.SetScopes(request.GetScopes());
                
            return controller.SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}