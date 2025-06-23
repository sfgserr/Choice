using BuildingBlocks.Application.Events;
using Chat.Application.ChatUsers.Commands.CreateChatUser;
using Chat.Infrastructure.Data.InternalCommands;
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

        public async Task Consume(UserCreatedIntegrationEvent @event)
        {
            await _scheduler.EnqueueAsync(new CreateChatUserCommand(
                @event.Id, 
                @event.UserId,
                @event.UserName,
                @event.DeviceName,
                @event.DeviceToken));
        }
    }
}