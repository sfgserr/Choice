using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.EnrolledWithPrepayment
{
    internal class EnrolledWithPrepaymentDomainNotificationHandler : 
        IDomainNotificationHandler<EnrolledWithPrepaymentDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal EnrolledWithPrepaymentDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(EnrolledWithPrepaymentDomainNotification notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new EnrolledWithPrepaymentIntegrationEvent(
                Guid.NewGuid(),
                notification.DomainEvent.ResponseId.Value));
        }
    }
}