using Microsoft.AspNetCore.Authorization;

namespace Identity.Infrastructure.Authorization
{
    public class HasPermissionRequirement : IAuthorizationRequirement
    {
        public HasPermissionRequirement(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
