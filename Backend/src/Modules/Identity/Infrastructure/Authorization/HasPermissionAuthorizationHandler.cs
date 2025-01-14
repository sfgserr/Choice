using Microsoft.AspNetCore.Authorization;

namespace Identity.Infrastructure.Authorization
{
    public class HasPermissionAuthorizationHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
        {
            var permissions = context.User.GetPermissions();

            if (Authorize(permissions, requirement.Code))
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }

        private bool Authorize(List<string> permissions, string code)
        {
            return permissions.Any(p => p == code);
        }
    }
}
