using Microsoft.AspNetCore.Authorization;

namespace BuildingBlocks.Infrastructure.Authorization
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
        {
            Permission = permission;
        }
        
        public string Permission { get; }
    }
}