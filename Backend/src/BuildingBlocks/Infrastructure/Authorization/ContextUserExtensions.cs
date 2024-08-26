using System.Security.Claims;

namespace BuildingBlocks.Infrastructure.Authorization
{
    internal static class ContextUserExtensions
    {
        public static List<string> GetPermissions(this ClaimsPrincipal claims)
        {
            return claims.FindAll(c => c.Type == "Permission").Select(c => c.Value).ToList();
        }
    }
}
