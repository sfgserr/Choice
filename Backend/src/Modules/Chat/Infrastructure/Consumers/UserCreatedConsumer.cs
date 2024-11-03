using BuildingBlocks.Application.Events;
using Chat.Application.ChatUsers.Commands.CreateChatUser;
using Chat.Infrastructure.Data.Domain.InternalCommands;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    internal class UserCreatedConsumer : IBusConsumer<UserCreatedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal UserCreatedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(UserCreatedIntegrationEvent integrationEvent)
        {
            await _scheduler.EnqueueAsync(new CreateChatUserCommand(
                integrationEvent.Id, 
                integrationEvent.UserId,
                integrationEvent.UserName));
        }
    }
}