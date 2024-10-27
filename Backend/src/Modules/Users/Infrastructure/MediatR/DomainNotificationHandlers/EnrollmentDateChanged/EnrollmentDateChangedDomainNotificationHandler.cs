using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.EnrollmentDateChanged
{
    internal class EnrollmentDateChangedDomainNotificationHandler :
        IDomainNotificationHandler<EnrollmentDateChangedDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal EnrollmentDateChangedDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(EnrollmentDateChangedDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            await _eventBus.PublishAsync(new EnrollmentDateChangedIntegrationEvent(
                domainEvent.Id,
                domainEvent.ResponseId.Value,
                domainEvent.PreviousEnrollmentDate,
                domainEvent.UserChangedEnrollmentDateId.Value,
                domainEvent.IsEnrollmentDateConfirmed));
        }
    }
}