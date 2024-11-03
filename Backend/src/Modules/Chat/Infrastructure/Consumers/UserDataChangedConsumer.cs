using BuildingBlocks.Application.Events;
using Chat.Application.ChatUsers.Commands.ChangeName;
using Chat.Infrastructure.Data.Domain.InternalCommands;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
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
            await _scheduler.EnqueueAsync(new ChangeNameCommand(
                integrationEvent.Id,
                integrationEvent.UserId,
                integrationEvent.Name));
        }
    }
}