using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.MarkAsPaid
{
    public class MarkAsPaidCommand : InternalCommandBase
    {
        public MarkAsPaidCommand(Guid id, Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}
