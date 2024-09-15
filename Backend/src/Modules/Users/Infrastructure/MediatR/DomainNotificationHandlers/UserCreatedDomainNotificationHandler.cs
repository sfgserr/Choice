using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers
{
    internal class UserCreatedDomainNotificationHandler : IDomainNotificationHandler<UserCreatedDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal UserCreatedDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(UserCreatedDomainNotification notification, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new UserCreatedIntegrationEvent(notification.DomainEvent.Id));
        }
    }
}
