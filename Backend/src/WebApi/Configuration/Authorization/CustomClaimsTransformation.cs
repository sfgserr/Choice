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
            var permissions = await _usersModule.Query<GetUserPermissionsQuery, IList<PermissionDto>>(new());

            var identity = new ClaimsIdentity();

            foreach (var permission in permissions)
                identity.AddClaim(new Claim("Permission", permission.Code));

            principal.AddIdentity(identity);

            return principal;
        }
    }
}
