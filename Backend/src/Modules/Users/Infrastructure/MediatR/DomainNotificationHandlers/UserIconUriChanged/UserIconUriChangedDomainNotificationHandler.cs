using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.UserIconUriChanged
{
    internal class UserIconUriChangedDomainNotificationHandler : IDomainNotificationHandler<UserIconUriChangedDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal UserIconUriChangedDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(UserIconUriChangedDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            await _eventBus.PublishAsync(new UserIconUriChangedIntegrationEvent(
                notification.Id,
                domainEvent.UserId.Value,
                domainEvent.IconUri));
        }
    }
}