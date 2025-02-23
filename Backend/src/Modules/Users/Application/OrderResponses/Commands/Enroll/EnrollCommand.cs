using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.Enroll
{
    public class EnrollCommand : InternalCommandBase
    {
        public EnrollCommand(Guid responseId, Guid id = default) : base(id)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}