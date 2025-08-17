using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Identity.Infrastructure.Authorization.Rules
{
    public class AuthorizationRuleService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IList<IAuthorizationRule> _rules;
        
        public AuthorizationRuleService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _rules = 
            [
                new SubscriptionForCompanyRequiredAuthorizationRule(),
                new UserMustNotBeBannedAuthorizationRule()
            ];
        }


        public bool EnsureAllRulesFollowed()
        {
            var rulesToSkip = _contextAccessor
                .HttpContext?
                .GetEndpoint()?
                .Metadata
                .GetMetadata<SkipAuthorizationRuleAttribute>()?
                .AuthorizationRuleNames ?? [];

            foreach (var rule in _rules)
                if (rulesToSkip.All(r => r != rule.Name))
                    if (!rule.IsFollowed(_contextAccessor.HttpContext?.User ?? new ClaimsPrincipal())) return false;

            return true;
        }
    }
}