using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.EnrollmentDateConfirmed
{
    internal class EnrollmentDateConfirmedDomainNotificationHandler : 
        IDomainNotificationHandler<EnrollmentDateConfirmedDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal EnrollmentDateConfirmedDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(EnrollmentDateConfirmedDomainNotification notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new EnrollmentDateConfirmedIntegrationEvent(
                Guid.NewGuid(),
                notification.DomainEvent.ResponseId.Value));
        }
    }
}