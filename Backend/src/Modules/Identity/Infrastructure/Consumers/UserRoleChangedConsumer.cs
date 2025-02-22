using BuildingBlocks.Application.Events;
using Identity.Application.Users.ChangeRole;
using Identity.Infrastructure.Data.InternalCommands;
using Users.IntegrationEvents;

namespace Identity.Infrastructure.Consumers
{
    internal class UserRoleChangedConsumer : IBusConsumer<UserRoleChangedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal UserRoleChangedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(UserRoleChangedIntegrationEvent @event)
        {
            await _scheduler.EnqueueAsync(new ChangeRoleCommand(
                @event.Id,
                @event.UserId,
                @event.UserRole));
        }
    }
}
