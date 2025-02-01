using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Chat.Commands.SendMessageCommand
{
    public class SendMessageCommand : InternalCommandBase
    {
        public SendMessageCommand(Guid id, Guid messageId) : base(id)
        {
            MessageId = messageId;
        }
        
        public Guid MessageId { get; }
    }
}