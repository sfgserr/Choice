using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.OrderResponseCreated
{
    internal class OrderResponseCreatedDomainNotificationHandler : IDomainNotificationHandler<OrderResponseCreatedDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal OrderResponseCreatedDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(OrderResponseCreatedDomainNotification notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new OrderResponseCreatedIntegrationEvent(
                Guid.NewGuid(),
                notification.DomainEvent.ResponseId.Value));
        }
    }
}
