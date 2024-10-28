using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.OrderPaid
{
    internal class PublishEnrolledIntegrationEventDomainNotificationHandler :
        IDomainNotificationHandler<OrderPaidDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal PublishEnrolledIntegrationEventDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(OrderPaidDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            await _eventBus.PublishAsync(new EnrolledIntegrationEvent(
                domainEvent.Id,
                domainEvent.ResponseId.Value,
                domainEvent.CompanyId.Value));
        }
    }
}
