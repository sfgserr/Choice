using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Processing;

namespace Users.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : ICommand, IRecurringCommand
    {
    }
}
