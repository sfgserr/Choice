using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Identity.Infrastructure.Authorization.Rules
{
    public interface IAuthorizationRule
    {
        string Name { get; }
        
        bool IsFollowed(ClaimsPrincipal user);
    }
}