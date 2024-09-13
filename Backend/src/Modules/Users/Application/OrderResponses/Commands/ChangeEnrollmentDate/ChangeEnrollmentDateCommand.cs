using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.ChangeEnrollmentDate
{
    public class ChangeEnrollmentDateCommand : ICommand
    {
        public ChangeEnrollmentDateCommand(Guid responseId, DateTime enrollmentDate)
        {
            ResponseId = responseId;
            EnrollmentDate = enrollmentDate;
        }

        public Guid ResponseId { get; }

        public DateTime EnrollmentDate { get; }
    }
}