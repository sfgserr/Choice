using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Chat.Domain.Messages.Events;

namespace Chat.Infrastructure.MediatR.DomainNotifications
{
    public class MessageCreatedDomainNotification : DomainNotificationBase<MessageCreatedDomainEvent>
    {
        public MessageCreatedDomainNotification(MessageCreatedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}