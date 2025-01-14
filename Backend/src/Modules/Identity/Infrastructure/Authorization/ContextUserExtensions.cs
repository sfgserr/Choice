using System.Security.Claims;

namespace Identity.Infrastructure.Authorization
{
    internal static class ContextUserExtensions
    {
        public static List<string> GetPermissions(this ClaimsPrincipal claims)
        {
            return claims.Claims.Where(c => c.Type == "permission").Select(c => c.Value).ToList();
        }
    }
}
