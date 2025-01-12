using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Identity.Infrastructure.Middlewares.SubscriptionCheck
{
    public class CheckActiveSubscriptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckActiveSubscriptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var role = context.User.FindFirst("role");

            if (role is { Value: "Company" })
            {
                if (context.GetEndpoint()?.Metadata.GetMetadata<AllowUnsubscribeAttribute>() == null)
                {
                    var subscribe = context.User.FindFirst("subscribed");

                    if (subscribe != null && subscribe.Value == "False")
                    {
                        await context.ForbidAsync();
                        return;
                    }
                } 
            }
            
            await _next(context);
        }
    }

    public static class CheckActiveSubscriptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSubscriptionCheck(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckActiveSubscriptionMiddleware>();
        }
    }
}