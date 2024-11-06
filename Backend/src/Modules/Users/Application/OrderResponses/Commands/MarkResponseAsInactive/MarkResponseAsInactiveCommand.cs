using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.MarkResponseAsInactive
{
    public class MarkResponseAsInactiveCommand : InternalCommandBase
    {
        public MarkResponseAsInactiveCommand(Guid id, Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }
        
        internal Guid ResponseId { get; }
    }
}