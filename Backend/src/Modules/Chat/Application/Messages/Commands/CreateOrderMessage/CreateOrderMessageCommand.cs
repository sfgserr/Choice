using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Messages.Commands.CreateOrderMessage
{
    public class CreateOrderMessageCommand : InternalCommandBase
    {
        public CreateOrderMessageCommand(
            Guid id, 
            Guid responseId, 
            Guid fromUserId, 
            Guid toUserId) : base(id)
        {
            ResponseId = responseId;
            FromUserId = fromUserId;
            ToUserId = toUserId;
        }

        internal Guid ResponseId { get; }
        
        internal Guid FromUserId { get; }
        
        internal Guid ToUserId { get; }
    }
}