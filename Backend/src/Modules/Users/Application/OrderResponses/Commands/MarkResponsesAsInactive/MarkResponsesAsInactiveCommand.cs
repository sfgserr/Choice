using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.MarkResponsesAsInactive
{
    public class MarkResponsesAsInactiveCommand : InternalCommandBase
    {
        public MarkResponsesAsInactiveCommand(Guid id, Guid responseId, Guid requestId) : base(id)
        {
            ResponseId = responseId;
            RequestId = requestId;
        }

        public Guid ResponseId { get; }

        public Guid RequestId { get; }
    }
}