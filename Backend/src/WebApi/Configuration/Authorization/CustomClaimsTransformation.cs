using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Identity.Application.Authorization.GetUser;
using Identity.Application.Contracts;

namespace WebApi.Configuration.Authorization
{
    public class CustomClaimsTransformation : IClaimsTransformation
    {
        private readonly IIdentityModule _identityModule;

        public CustomClaimsTransformation(IIdentityModule identityModule)
        {
            _identityModule = identityModule;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {   
            var user = await _identityModule
                .Query<GetUserQuery, UserDto>(new(GetId(principal)));

            var identity = new ClaimsIdentity();

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
            var id = principal.Claims.FirstOrDefault(c => c.Type == "id");

            if (id is null)
            {
                throw new ApplicationException("No Id in claims");
            }

            return Guid.TryParse(id.Value, out var parsedId) ? 
                parsedId : throw new ApplicationException("User Id is not guid");
        }
    }
}
