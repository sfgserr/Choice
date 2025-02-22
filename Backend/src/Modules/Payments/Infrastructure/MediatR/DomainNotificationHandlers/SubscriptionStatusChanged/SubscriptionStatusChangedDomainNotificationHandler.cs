using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Payments.Infrastructure.MediatR.DomainNotifications;
using Payments.IntegrationEvents;

namespace Payments.Infrastructure.MediatR.DomainNotificationHandlers.SubscriptionStatusChanged
{
    internal class SubscriptionStatusChangedDomainNotificationHandler : 
        IDomainNotificationHandler<SubscriptionStatusChangedDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal SubscriptionStatusChangedDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(SubscriptionStatusChangedDomainNotification notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new SubscriptionStatusChangedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.SubscriberId.Value));
        }
    }
}