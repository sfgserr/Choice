using Chat.Domain.Messages;
using Chat.Domain.Messages.OrderMessages;

namespace Chat.Application.Messages
{
    internal class MessageDataModel
    {
        public MessageType Type { get; }

        public OrderMessage? OrderMessage { get; }

        public DateTime CreationDate { get; }
    }
}