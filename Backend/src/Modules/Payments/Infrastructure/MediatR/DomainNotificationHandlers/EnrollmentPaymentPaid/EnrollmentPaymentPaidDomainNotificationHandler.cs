using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Payments.Infrastructure.MediatR.DomainNotifications;
using Payments.IntegrationEvents;

namespace Payments.Infrastructure.MediatR.DomainNotificationHandlers.EnrollmentPaymentPaid
{
    internal class EnrollmentPaymentPaidDomainNotificationHandler : 
        IDomainNotificationHandler<EnrollmentPaymentPaidDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal EnrollmentPaymentPaidDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(EnrollmentPaymentPaidDomainNotification notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new EnrollmentPaidIntegrationEvent(
                Guid.NewGuid(),
                notification.DomainEvent.ResponseId.Value));
        }
    }
}
