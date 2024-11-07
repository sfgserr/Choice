using Administration.IntegrationEvents;
using BuildingBlocks.Application.Events;
using Chat.Application.ChatUsers.Commands.DeleteUserCommand;
using Chat.Infrastructure.Data.InternalCommands;

namespace Chat.Infrastructure.Consumers
{
    internal class UserDeletedConsumer : IBusConsumer<ClientDeletedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal UserDeletedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(ClientDeletedIntegrationEvent integrationEvent)
        {
            await _scheduler.EnqueueAsync(new DeleteUserCommand(
                integrationEvent.Id,
                integrationEvent.ClientId));
        }
    }
}