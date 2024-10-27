using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Messages.Commands.ChangeEnrollmentDate
{
    public class ChangeEnrollmentDateCommand : InternalCommandBase
    {
        public ChangeEnrollmentDateCommand(
            Guid id,
            Guid responseId, 
            DateTime enrollmentDate) : base(id)
        {
            ResponseId = responseId;
            EnrollmentDate = enrollmentDate;
        }

        public Guid ResponseId { get; }
        
        public DateTime EnrollmentDate { get; }
    }
}