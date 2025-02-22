using BuildingBlocks.Application.Events;
using Chat.Application.ChatUsers.Commands.ChangeIconUri;
using Chat.Infrastructure.Data.InternalCommands;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    internal class UserIconUriConsumer : IBusConsumer<UserIconUriChangedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal UserIconUriConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(UserIconUriChangedIntegrationEvent @event)
        {
            await _scheduler.EnqueueAsync(new ChangeIconUriCommand(
                @event.Id,
                @event.UserId,
                @event.IconUri));
        }
    }
}