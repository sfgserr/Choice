
using Chat.Api.Entities;

namespace Choice.Chat.Api.Entities
{
    public class Message
    {
        public Message(string senderId, string receiverId, string body, MessageType type)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Body = body;
            Type = type;
        }

        public int Id { get; }
        public string SenderId { get; }
        public string ReceiverId { get; }
        public User? Receiver { get; set; }
        public string Body { get; }
        public MessageType Type { get; }
        public DateTime CreationTime { get; } = DateTime.Now;
    }

    public enum MessageType
    {
        Text = 1,
        Image = 2,
        Order = 3,
    }
}
