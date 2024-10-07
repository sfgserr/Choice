using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Processing;

namespace Payments.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : IRecurringCommand, ICommand
    {
    }
}
