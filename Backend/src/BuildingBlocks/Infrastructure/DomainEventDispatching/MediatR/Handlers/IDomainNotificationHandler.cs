using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using MediatR;

namespace BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers
{
    public interface IDomainNotificationHandler<TDomainNotification> :
        INotificationHandler<TDomainNotification> where TDomainNotification : IDomainNotification
    {

    }
}
