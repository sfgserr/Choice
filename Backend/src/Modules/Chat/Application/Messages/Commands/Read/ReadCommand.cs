using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Messages.Commands.Read
{
    public class ReadCommand : ICommand
    {
        public ReadCommand(Guid userId, Guid messageId)
        {
            UserId = userId;
            MessageId = messageId;
        }
        
        public Guid UserId { get; }
        
        public Guid MessageId { get; }
    }
}