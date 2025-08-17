using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Identity.Infrastructure.Authorization.Rules
{
    public class SubscriptionForCompanyRequiredAuthorizationRule : IAuthorizationRule
    {
        public string Name => "SubscriptionForCompanyRequired";

        public bool IsFollowed(ClaimsPrincipal user)
        {
            var role = user.FindFirst("role");

            if (role is { Value: "Company" })
            {
                var subscribe = user.FindFirst("subscribed");

                return subscribe != null && subscribe.Value == "True";
            }

            return true;
        }
    }
}