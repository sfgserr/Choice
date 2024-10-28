using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Chat.SendOrderMessage
{
    public class SendOrderMessageCommand : ICommand
    {
        public SendOrderMessageCommand(Guid responseId, Guid toUserId)
        {
            ResponseId = responseId;
            ToUserId = toUserId;
        }

        public Guid ResponseId { get; }
        
        public Guid ToUserId { get; }
    }
}