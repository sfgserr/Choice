using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Configuration.Identity
{
    public static class IdentityExtensions
    {
        public static void AddIdentity(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddOpenIddict()
                .AddCore();
            
            services.AddOpenIddict()
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("api/auth/login")
                           .AllowPasswordFlow()
                           .AllowRefreshTokenFlow()
                           .UseAspNetCore()
                           .EnableTokenEndpointPassthrough();

                    options.AddEncryptionKey(
                        new SymmetricSecurityKey(Convert.FromBase64String(jwtOptions.SecretKey)));
                    
                    options.AddDevelopmentSigningCertificate();
                })
                .AddValidation(options =>
                {
                    options.SetIssuer(jwtOptions.Issuer);
                    
                    options.AddEncryptionKey(
                        new SymmetricSecurityKey(Convert.FromBase64String(jwtOptions.SecretKey)));

                    options.UseLocalServer();
                    
                    options.UseAspNetCore();
                });
        }
    }
}