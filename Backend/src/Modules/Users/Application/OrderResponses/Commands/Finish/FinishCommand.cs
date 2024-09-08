using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.Finish
{
    public class FinishCommand : ICommand
    {
        public FinishCommand(Guid responseId)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}