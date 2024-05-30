
namespace Choice.Chat.Api.ViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel(string name, string iconUri, string guid, 
            List<MessageViewModel> messages, int status, DateTime? lastTimeOnline)
        {
            Name = name;
            IconUri = iconUri;
            Guid = guid;
            Messages = messages;
            Status = status;
            LastTimeOnline = lastTimeOnline;
        }

        public string Name { get; }
        public string IconUri { get; }
        public string Guid { get; }
        public int Status { get; }
        public DateTime? LastTimeOnline { get; }
        public List<MessageViewModel> Messages { get; }
    }
}
