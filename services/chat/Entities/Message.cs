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
            Body = body;

            Content = type == MessageType.Order ? new OrderContent(body) : new DefaultContent(body);
            Content.BodyChanged += OnBodyChanged;
        }

        protected Message() { }

        public int Id { get; }
        public string SenderId { get; private set; }
        public string ReceiverId { get; private set; }
        public object Body { get; private set; }
        public MessageType Type { get; private set; }
        public bool IsRead { get; private set; } = false;
        public DateTime CreationTime { get; private set; } = DateTime.Now;

        public IContent Content { get; }

        public void Read()
        {
            IsRead = true;
        }

        public void OnBodyChanged(object body)
        {
            Body = body;
        }
    }

    public enum MessageType
    {
        Text = 1,
        Image = 2,
        Order = 3,
    }
}
