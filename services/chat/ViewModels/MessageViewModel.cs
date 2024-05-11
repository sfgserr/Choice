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
            ReceiverId = message.Receiver?.Guid!;
            IconUri = message.Receiver?.IconUri!;
            CreationTime = message.CreationTime;
        }

        public int Id { get; }
        public string Body { get; }
        public string SenderId { get; }
        public string ReceiverId { get; }
        public string IconUri { get; }
        public DateTime CreationTime { get; }
    }
}
