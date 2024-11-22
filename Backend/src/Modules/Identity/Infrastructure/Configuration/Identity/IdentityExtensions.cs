using Identity.Infrastructure.Authorization;
using Identity.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Configuration.Identity
{
    public static class IdentityExtensions
    {
        public static void AddIdentity(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                           .UseDbContext<IdentityContext>();
                });
            
            services.AddOpenIddict()
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("api/auth/login")
                           .AllowPasswordFlow()
                           .AllowRefreshTokenFlow()
                           .UseAspNetCore()
                           .EnableTokenEndpointPassthrough();

                    options.AddDevelopmentSigningCertificate()
                           .AddDevelopmentEncryptionCertificate();
                    
                    options.DisableAccessTokenEncryption();
                })
                .AddValidation(options =>
                {
                    options.SetIssuer(jwtOptions.Issuer);

                    options.UseLocalServer();
                    
                    options.UseAspNetCore();
                });
        }
    }
}