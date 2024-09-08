using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.Enroll
{
    public class EnrollCommand : ICommand
    {
        public EnrollCommand(Guid responseId)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}