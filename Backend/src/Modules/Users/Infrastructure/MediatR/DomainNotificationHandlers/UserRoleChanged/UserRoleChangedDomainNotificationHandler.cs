using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.UserRoleChanged
{
    internal class UserRoleChangedDomainNotificationHandler : IDomainNotificationHandler<UserRoleChangedDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal UserRoleChangedDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(UserRoleChangedDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            await _eventBus.PublishAsync(new UserRoleChangedIntegrationEvent(
                Guid.NewGuid(),
                domainEvent.UserId.Value,
                domainEvent.UserRole.Value));
        }
    }
}
