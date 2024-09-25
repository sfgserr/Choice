using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.UserDataChanged
{
    internal class UserDataChangedDomainNotificationHandler : IDomainNotificationHandler<UserDataChangedDomainNotification>
    {
        private readonly IEventBus _eventBus;

        internal UserDataChangedDomainNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(UserDataChangedDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            await _eventBus.PublishAsync(new UserDataChangedIntegrationEvent(
                Guid.NewGuid(),
                domainEvent.UserId.Value,
                domainEvent.Name,
                domainEvent.Email,
                domainEvent.PhoneNumber,
                domainEvent.Address.City,
                domainEvent.Address.Street,
                domainEvent.Address.Coords.Latitude,
                domainEvent.Address.Coords.Longitude));
        }
    }
}
