using BuildingBlocks.Domain;

namespace Chat.Domain.ChatUsers
{
    public class ChatUser : Entity, IAggregateRoot
    {
        private string _name;

        private string _iconUri;

        private bool _isDeleted;
        
        private ChatUser()
        {

        }

        private ChatUser(
            ChatUserId id,
            string name,
            string iconUri,
            bool isDeleted)
        {
            Id = id;
            
            _name = name;
            _iconUri = iconUri;
            _isDeleted = isDeleted;
        }

        public static ChatUser Create(
            ChatUserId id,
            string name,
            string iconUri)
        {
            return new ChatUser(id, name, iconUri, false);
        }

        public ChatUserId Id { get; }

        public void ChangeName(string name)
        {
            _name = name;
        }
        
        public void ChangeIconUri(string iconUri)
        {
            _iconUri = iconUri;
        }

        public void Delete()
        {
            _name = "Deleted user";
            _iconUri = "deleted";
            
            _isDeleted = true;
        }
    }
}