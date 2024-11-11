using BuildingBlocks.Domain;

namespace Chat.Domain.ChatUsers
{
    public class ChatUser : Entity, IAggregateRoot
    {
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
            Name = name;
            IconUri = iconUri;
            IsDeleted = isDeleted;
        }

        public static ChatUser Create(
            ChatUserId id,
            string name,
            string iconUri)
        {
            return new ChatUser(id, name, iconUri, false);
        }

        public ChatUserId Id { get; }

        public string Name { get; private set; }

        public string IconUri { get; private set; }

        public bool IsDeleted { get; private set; }

        public void ChangeName(string name)
        {
            Name = name;
        }
        
        public void ChangeIconUri(string iconUri)
        {
            IconUri = iconUri;
        }

        public void Delete()
        {
            Name = "Deleted user";
            IconUri = "deleted";
            
            IsDeleted = true;
        }
    }
}