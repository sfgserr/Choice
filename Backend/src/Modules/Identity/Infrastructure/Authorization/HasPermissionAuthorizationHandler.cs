using Identity.Infrastructure.Authorization.Rules;
using Microsoft.AspNetCore.Authorization;

namespace Identity.Infrastructure.Authorization
{
    public class HasPermissionAuthorizationHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        private readonly AuthorizationRuleService _authorizationRuleService;

        public HasPermissionAuthorizationHandler(
            AuthorizationRuleService authorizationRuleService)
        {
            _authorizationRuleService = authorizationRuleService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
        {
            var permissions = context.User.GetPermissions();
            
            var allRulesFollowed = _authorizationRuleService.EnsureAllRulesFollowed();
            
            if (Authorize(permissions, requirement.Code) && allRulesFollowed)
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }

        private bool Authorize(List<string> permissions, string code)
        {
            return permissions.Any(p => p == code);
        }
    }
}
