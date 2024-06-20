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

            SetContent();
        }

        protected Message() { }

        public int Id { get; }
        public string SenderId { get; private set; }
        public string ReceiverId { get; private set; }
        public string Body { get; private set; }
        public MessageType Type { get; private set; }
        public bool IsRead { get; private set; } = false;
        public DateTime CreationTime { get; private set; } = DateTime.Now;

        public IContent Content { get; private set; }

        public void SetContent()
        {
            if (Content is null)
            {
                Content = Type == MessageType.Order ? new OrderContent(Body) : new DefaultContent(Body);
                Content.BodyChanged += OnBodyChanged;
            }
        }

        public void Read()
        {
            IsRead = true;
        }

        public void OnBodyChanged(string body)
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
