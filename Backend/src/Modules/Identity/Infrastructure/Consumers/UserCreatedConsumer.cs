using Identity.Infrastructure.Data.InternalCommands;
using MassTransit;
using Users.IntegrationEvents;

namespace Identity.Infrastructure.Consumers
{
    internal class UserCreatedConsumer : IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        public UserCreatedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            var @event = context.Message;
        }
    }
}