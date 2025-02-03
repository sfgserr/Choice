using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Chat.Commands.SendOrderMessage
{
    public class SendOrderMessageCommand : ICommand
    {
        public SendOrderMessageCommand(object data, Guid toUserId)
        {
            Data = data;
            ToUserId = toUserId;
        }
        
        public object Data { get; }
        
        public Guid ToUserId { get; }
    }
}