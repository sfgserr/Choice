using MediatR;

namespace BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications
{
    public interface IDomainNotification : INotification
    {
        Guid Id { get; }
    }
}
