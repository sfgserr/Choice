using Choice.Chat.Api.Content;
using Choice.Chat.Api.Content.Interfaces;

namespace Choice.Chat.Api.Entities
{
    public class Message
    {
        public Message(string senderId, string receiverId, string body, MessageType type)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Type = type;
            Content = type switch
            {
                MessageType.Order => new OrderContent(body),
                _ => new DefaultContent(body)
            };
        }

        protected Message() { }

        public int Id { get; }
        public string SenderId { get; }
        public string ReceiverId { get; }
        public User? Receiver { get; set; }
        public string Body => Content.Content;
        public MessageType Type { get; }
        public bool IsRead { get; private set; } = false;
        public DateTime CreationTime { get; } = DateTime.Now;
        public IContent Content { get; }

        public void Read()
        {
            IsRead = true;
        }
    }

    public enum MessageType
    {
        Text = 1,
        Image = 2,
        Order = 3,
    }
}
