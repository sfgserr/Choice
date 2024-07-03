
namespace Choice.Chat.Api.ViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel(
            string name, 
            string iconUri, 
            string guid,
            bool isDeleted,
            List<MessageViewModel> messages, 
            int status, 
            DateTime? lastTimeOnline)
        {
            Name = name;
            IconUri = iconUri;
            Guid = guid;
            IsDeleted = isDeleted;
            Messages = messages;
            Status = status;
            LastTimeOnline = lastTimeOnline;
        }

        public string Name { get; }
        public string IconUri { get; }
        public string Guid { get; }
        public bool IsDeleted { get; }
        public int Status { get; }
        public DateTime? LastTimeOnline { get; }
        public List<MessageViewModel> Messages { get; }
    }
}
