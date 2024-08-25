using MediatR;

namespace BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR
{
    public interface IDomainNotification : INotification
    {
        Guid Id { get; }
    }
}
