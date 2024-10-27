using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Chat.SendOrderMessage
{
    public class SendOrderMessageCommand : ICommand
    {
        public SendOrderMessageCommand(Guid responseId)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}