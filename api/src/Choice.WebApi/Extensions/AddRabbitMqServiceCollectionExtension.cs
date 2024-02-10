using MassTransit;

namespace Choice.WebApi.Extensions
{
    public static class AddRabbitMqServiceCollectionExtension
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBus:Host"]);
                });
            });

            return services;
        }
    }
}
