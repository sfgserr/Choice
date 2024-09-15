using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers
{
    internal class PublishOrderStatusChangedEventDomainNotificationHandler :
        IDomainNotificationHandler<OrderStatusChangedDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal PublishOrderStatusChangedEventDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(OrderStatusChangedDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            await _eventBus.PublishAsync(new OrderStatusChangedIntegrationEvent(
                domainEvent.ResponseId.Value,
                domainEvent.Status.Value));
        }
    }
}