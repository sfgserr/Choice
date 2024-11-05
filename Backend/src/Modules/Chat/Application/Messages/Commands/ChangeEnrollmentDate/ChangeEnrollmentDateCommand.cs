using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Messages.Commands.ChangeEnrollmentDate
{
    public class ChangeEnrollmentDateCommand : InternalCommandBase
    {
        public ChangeEnrollmentDateCommand(
            Guid id,
            Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}