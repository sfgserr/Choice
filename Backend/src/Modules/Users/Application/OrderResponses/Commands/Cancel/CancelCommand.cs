using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.Cancel
{
    public class CancelCommand : ICommand
    {
        public CancelCommand(Guid responseId)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}