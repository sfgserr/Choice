using BuildingBlocks.Application.Cqrs.Commands;

namespace BuildingBlocks.Infrastructure.InternalCommands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(InternalCommandBase command);
    }
}
