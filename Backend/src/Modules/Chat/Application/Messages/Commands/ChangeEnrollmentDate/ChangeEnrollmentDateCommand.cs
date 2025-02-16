using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Messages.Commands.ChangeEnrollmentDate
{
    public class ChangeEnrollmentDateCommand : InternalCommandBase
    {
        public ChangeEnrollmentDateCommand(
            Guid id,
            DateTime enrollmentDate,
            Guid responseId, 
            Guid toUserId) : base(id)
        {
            ResponseId = responseId;
            EnrollmentDate = enrollmentDate;
            ToUserId = toUserId;
        }

        public Guid ResponseId { get; }
        
        public DateTime EnrollmentDate { get; }
        
        public Guid ToUserId { get; }
    }
}