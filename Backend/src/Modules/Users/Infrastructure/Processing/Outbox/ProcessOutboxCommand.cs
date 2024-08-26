using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Processing;

namespace Users.Infrastructure.Processing.Outbox
{
    internal class ProcessOutboxCommand : ICommand, IRecurringCommand
    {
    }
}
