using BuildingBlocks.Application.Events;
using Identity.Application.Users.ChangeData;
using Identity.Infrastructure.Data.InternalCommands;
using Users.IntegrationEvents;

namespace Identity.Infrastructure.Consumers
{
    internal class UserDataChangedConsumer : IBusConsumer<UserDataChangedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal UserDataChangedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(UserDataChangedIntegrationEvent @event)
        {
            await _scheduler.EnqueueAsync(new ChangeDataCommand(
                @event.Id,
                @event.UserId,
                @event.Email,
                @event.PhoneNumber,
                @event.City,
                @event.Street,
                @event.Latitude,
                @event.Longitude));
        }
    }
}
