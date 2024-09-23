using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.OrderRequests.OrderResponses.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class OrderPaidDomainNotification : DomainNotificationBase<OrderPaidDomainEvent>
    {
        public OrderPaidDomainNotification(OrderPaidDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}