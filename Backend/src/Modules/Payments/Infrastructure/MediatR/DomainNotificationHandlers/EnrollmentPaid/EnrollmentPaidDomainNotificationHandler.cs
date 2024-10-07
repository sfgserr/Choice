using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Payments.Infrastructure.MediatR.DomainNotifications;
using Payments.IntegrationEvents;

namespace Payments.Infrastructure.MediatR.DomainNotificationHandlers.EnrollmentPaid
{
    internal class EnrollmentPaidDomainNotificationHandler : IDomainNotificationHandler<EnrollmentPaidDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal EnrollmentPaidDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(EnrollmentPaidDomainNotification notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new EnrollmentPaidIntegrationEvent(
                Guid.NewGuid(),
                notification.DomainEvent.ResponseId.Value));
        }
    }
}
