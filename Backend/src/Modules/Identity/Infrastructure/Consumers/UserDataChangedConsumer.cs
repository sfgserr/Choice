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

        public async Task Consume(UserDataChangedIntegrationEvent integrationEvent)
        {
            await _scheduler.EnqueueAsync(new ChangeDataCommand(
                integrationEvent.Id,
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.PhoneNumber,
                integrationEvent.City,
                integrationEvent.Street,
                integrationEvent.Latitude,
                integrationEvent.Longitude));
        }
    }
}
