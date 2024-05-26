using Choice.Chat.Api.Entities;

namespace Choice.Chat.Api.ViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel(Message message)
        {
            Id = message.Id;
            Body = message.Body;
            SenderId = message.SenderId;
            ReceiverId = message.ReceiverId;
            CreationTime = message.CreationTime;
            Type = (int)message.Type;
        }

        public int Id { get; }
        public object Body { get; }
        public string SenderId { get; }
        public string ReceiverId { get; }
        public int Type { get; }
        public DateTime CreationTime { get; }
    }
}
