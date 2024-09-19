using Identity.Infrastructure.Consumers;
using MassTransit;

namespace Identity.Infrastructure.Configuration.EventBus
{
    public static class MassTransitExtensions
    {
        public static void AddIdentityConsumers(this IBusRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<UserCreatedConsumer>();
        }
    }
}