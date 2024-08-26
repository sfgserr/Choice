using Microsoft.AspNetCore.Authorization;

namespace BuildingBlocks.Infrastructure.Authorization
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
