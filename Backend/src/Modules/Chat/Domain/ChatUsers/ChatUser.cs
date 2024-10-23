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
            string iconUri,
            bool isDeleted)
        {
            Id = id;
            IconUri = iconUri;
            IsDeleted = isDeleted;
        }

        public static ChatUser Create(
            ChatUserId id,
            string iconUri)
        {
            return new ChatUser(id, iconUri, false);
        }

        public ChatUserId Id { get; }

        public string IconUri { get; private set; }

        public bool IsDeleted { get; private set; }

        public void ChangeIconUri(string iconUri)
        {
            IconUri = iconUri;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}