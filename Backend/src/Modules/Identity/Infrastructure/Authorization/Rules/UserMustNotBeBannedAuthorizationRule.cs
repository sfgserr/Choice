using System.Security.Claims;

namespace Identity.Infrastructure.Authorization.Rules
{
    public class UserMustNotBeBannedAuthorizationRule : IAuthorizationRule
    {
        public string Name => "UserMustNotBeBanned";
        
        public bool IsFollowed(ClaimsPrincipal user)
        {
            var banned = user.FindFirst("banned");

            return banned is { Value: "False" };
        }
    }
}