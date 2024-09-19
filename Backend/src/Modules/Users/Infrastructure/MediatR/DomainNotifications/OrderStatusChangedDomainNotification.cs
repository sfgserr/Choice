using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.OrderRequests.OrderResponses.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class OrderStatusChangedDomainNotification : DomainNotificationBase<OrderStatusChangedDomainEvent>
    {
        public OrderStatusChangedDomainNotification(OrderStatusChangedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
