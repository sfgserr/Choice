using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Chat.Application.Chat.Commands.SendMessageCommand;
using Chat.Infrastructure.Data.InternalCommands;
using Chat.Infrastructure.MediatR.DomainNotifications;

namespace Chat.Infrastructure.MediatR.DomainNotificationHandlers.MessageCreated
{
    internal class EnqueueSendMessageDomainNotificationHandler : IDomainNotificationHandler<MessageCreatedDomainNotification>
    {
        private readonly CommandsScheduler _scheduler;

        internal EnqueueSendMessageDomainNotificationHandler(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Handle(MessageCreatedDomainNotification notification, CancellationToken cancellationToken)
        {
            await _scheduler.EnqueueAsync(new SendMessageCommand(
                notification.Id, 
                notification.DomainEvent.MessageId.Value));
        }
    }
}