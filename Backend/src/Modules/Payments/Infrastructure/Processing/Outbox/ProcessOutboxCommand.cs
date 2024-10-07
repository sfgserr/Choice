using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Processing;

namespace Payments.Infrastructure.Processing.Outbox
{
    internal class ProcessOutboxCommand : IRecurringCommand, ICommand 
    {

    }
}
