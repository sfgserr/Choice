using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Users.Application.Contracts;
using Users.Application.Users.Queries.GetUserPermissions;

namespace WebApi.Configuration.Authorization
{
    public class CustomClaimsTransformation : IClaimsTransformation
    {
        private readonly IUsersModule _usersModule;

        public CustomClaimsTransformation(IUsersModule usersModule)
        {
            _usersModule = usersModule;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {   
            var permissions = await _usersModule
                .Query<GetUserPermissionsQuery, IList<PermissionDto>>(new(GetId(principal)));

            var identity = new ClaimsIdentity();

            foreach (var permission in permissions)
                identity.AddClaim(new Claim("Permission", permission.Code));

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
