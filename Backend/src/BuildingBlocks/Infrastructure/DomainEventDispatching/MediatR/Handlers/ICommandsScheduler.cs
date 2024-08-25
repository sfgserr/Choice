using BuildingBlocks.Application.Cqrs.Commands;

namespace BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(InternalCommandBase command);
    }
}
