using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.ConfirmDate
{
    public class ConfirmDateCommand : ICommand
    {
        public ConfirmDateCommand(Guid responseId)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}
