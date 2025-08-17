namespace Identity.Infrastructure.Authorization
{
    public class SkipAuthorizationRuleAttribute : Attribute
    {
        public SkipAuthorizationRuleAttribute(params string[] authorizationRuleNames)
        {
            AuthorizationRuleNames = authorizationRuleNames;
        }

        public string[] AuthorizationRuleNames { get; }
    }
}