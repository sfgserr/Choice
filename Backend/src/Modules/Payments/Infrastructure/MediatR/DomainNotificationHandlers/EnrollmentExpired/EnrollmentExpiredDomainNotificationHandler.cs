using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Payments.Infrastructure.MediatR.DomainNotifications;
using Payments.IntegrationEvents;

namespace Payments.Infrastructure.MediatR.DomainNotificationHandlers.EnrollmentExpired
{
    internal class EnrollmentExpiredDomainNotificationHandler : 
        IDomainNotificationHandler<EnrollmentExpiredDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal EnrollmentExpiredDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(EnrollmentExpiredDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            await _eventBus.PublishAsync(new EnrollmentExpiredIntegrationEvent(
                domainEvent.Id,
                domainEvent.ResponseId.Value));
        }
    }
}