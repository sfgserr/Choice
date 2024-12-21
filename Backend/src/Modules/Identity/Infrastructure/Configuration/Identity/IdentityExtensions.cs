using Identity.Infrastructure.Authorization;
using Identity.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.Infrastructure.Configuration.Identity
{
    public static class IdentityExtensions
    {
        public static void AddIdentity(this IServiceCollection services, JwtOptions jwtOptions, IWebHostEnvironment env)
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
                    options.SetTokenEndpointUris("api/auth/token")
                           .AllowPasswordFlow()
                           .AllowRefreshTokenFlow();

                    if (env.IsDevelopment())
                    {
                        options.UseAspNetCore()
                               .EnableTokenEndpointPassthrough()
                               .DisableTransportSecurityRequirement();

                        options.SetAccessTokenLifetime(TimeSpan.FromMinutes(1));
                    }
                    else
                    {
                        options.UseAspNetCore()
                            .EnableTokenEndpointPassthrough();
                    }
                    
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