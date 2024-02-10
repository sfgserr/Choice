using Microsoft.IdentityModel.Tokens;

namespace Choice.WebApi.Extensions
{
    public static class AddAuthenticationServiceCollectionExtension
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["Authentication:Issuer"],
                        ValidAudience = configuration["Authentication:Audience"]
                    };
                });

            return services;
        }
    }
}
