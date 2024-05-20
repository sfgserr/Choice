
namespace Choice.Chat.Api.ViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel(string name, string iconUri, string guid, 
            List<MessageViewModel> messages)
        {
            Name = name;
            IconUri = iconUri;
            Guid = guid;
            Messages = messages;
        }

        public string Name { get; }
        public string IconUri { get; }
        public string Guid { get; }
        public List<MessageViewModel> Messages { get; }
    }
}
