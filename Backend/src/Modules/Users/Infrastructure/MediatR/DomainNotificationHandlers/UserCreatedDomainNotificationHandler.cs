using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers
{
    internal class UserCreatedDomainNotificationHandler : IDomainNotificationHandler<UserCreatedDomainNotification>
    {
        public Task Handle(UserCreatedDomainNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
