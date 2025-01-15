using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Identity.Application.Authorization.GetUser;
using Identity.Application.Contracts;
using Payments.Application.Contracts;
using Payments.Application.Subscriptions.Queries.CheckActiveSubscription;

namespace WebApi.Configuration.Authorization
{
    public class CustomClaimsTransformation : IClaimsTransformation
    {
        private readonly IIdentityModule _identityModule;
        private readonly IPaymentsModule _paymentsModule;
        
        public CustomClaimsTransformation(IIdentityModule identityModule, IPaymentsModule paymentsModule)
        {
            _identityModule = identityModule;
            _paymentsModule = paymentsModule;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var id = GetId(principal);
            
            var user = await _identityModule
                .Query<GetUserQuery, UserDto>(new(id));

            var identity = new ClaimsIdentity();

            if (user.RoleCode == "Company")
            {
                var subscribed = await _paymentsModule.Query<CheckActiveSubscriptionQuery, bool>(
                    new CheckActiveSubscriptionQuery(id));
                
                identity.AddClaim(new Claim("subscribed", subscribed.ToString().ToLower()));
            }
            
            foreach (var permission in user.Permissions)
                identity.AddClaim(new Claim("permission", permission));
            
            identity.AddClaim(new Claim("role", user.RoleCode));
            identity.AddClaim(new Claim("city", user.City));
            identity.AddClaim(new Claim("street", user.Street));
            identity.AddClaim(new Claim("latitude", user.Latitude));
            identity.AddClaim(new Claim("longitude", user.Longitude));
            
            principal.AddIdentity(identity);

            return principal;
        }

        private Guid GetId(ClaimsPrincipal principal)
        {
            var id = principal.Claims.FirstOrDefault(c => c.Type == "sub");

            ArgumentNullException.ThrowIfNull(id);

            return Guid.TryParse(id.Value, out var parsedId) ? 
                parsedId : throw new ApplicationException("User Id is not guid");
        }
    }
}
