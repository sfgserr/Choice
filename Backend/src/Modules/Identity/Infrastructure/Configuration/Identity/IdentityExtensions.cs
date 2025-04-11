using System.Security.Cryptography.X509Certificates;
using Identity.Infrastructure.Authorization;
using Identity.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Configuration.Identity
{
    public static class IdentityExtensions
    {
        public static void AddIdentity(this IServiceCollection services, IdentityOptions identityOptions, IWebHostEnvironment env)
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
                           .AllowRefreshTokenFlow()
                           .AllowCustomFlow("password_phone");
                    
                    options.UseAspNetCore()
                           .EnableTokenEndpointPassthrough()
                           .DisableTransportSecurityRequirement();
                    
                    if (env.IsDevelopment())
                    {
                        options.AddDevelopmentSigningCertificate();

                        options.SetAccessTokenLifetime(TimeSpan.FromMinutes(2));
                    }
                    else
                    {   
                        options.AddSigningCertificate(
                            new FileStream(identityOptions.PathToCert, FileMode.Open), 
                            identityOptions.CertificatePassword);
                    }

                    options.AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String(identityOptions.SecretKey)));
                    
                    options.DisableAccessTokenEncryption();
                    
                    options.SetIssuer(identityOptions.Issuer);
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    
                    options.UseAspNetCore();
                    
                    options.SetIssuer(identityOptions.Issuer);
                });
        }
    }
}