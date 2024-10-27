using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.Enrolled
{
    internal class EnrolledDomainNotificationHandler : IDomainNotificationHandler<EnrolledDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal EnrolledDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(EnrolledDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            await _eventBus.PublishAsync(new EnrolledIntegrationEvent(
                domainEvent.Id,
                domainEvent.ResponseId.Value));
        }
    }
}