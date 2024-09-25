using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.OrderRequests.OrderResponses.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class OrderResponseCreatedDomainNotification : DomainNotificationBase<OrderResponseCreatedDomainEvent>
    {
        public OrderResponseCreatedDomainNotification(OrderResponseCreatedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
