using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;
using Users.IntegrationEvents;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.UserCreated
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
            var domainEvent = notification.DomainEvent;
            
            await _eventBus.PublishAsync(new UserCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.UserId.Value,
                domainEvent.UserName,
                domainEvent.Email,
                domainEvent.Password,
                domainEvent.PhoneNumber,
                domainEvent.Role,
                domainEvent.Address.City,
                domainEvent.Address.Street,
                domainEvent.Address.Coords.Latitude,
                domainEvent.Address.Coords.Longitude,
                domainEvent.DeviceName,
                domainEvent.DeviceToken));
        }
    }
}
