using BuildingBlocks.Domain;
using Chat.Domain.ChatUsers.Events;

namespace Chat.Domain.ChatUsers
{
    public class ChatUser : Entity, IAggregateRoot
    {
        private ChatUser()
        {

        }

        private ChatUser(
            ChatUserId id,
            UserStatus status,
            bool isDeleted)
        {
            Id = id;
            Status = status;
            IsDeleted = isDeleted;
        }

        public static ChatUser Create(
            ChatUserId id)
        {
            return new ChatUser(id, UserStatus.Online, false);
        }

        public ChatUserId Id { get; }

        public UserStatus Status { get; private set; }
        
        public DateTime? LastTimeOnline { get; private set; }

        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void ChangeStatus()
        {
            Status = Status.Equals(UserStatus.Online) ? UserStatus.Offline : UserStatus.Online;
            LastTimeOnline = DateTime.Now;

            AddDomainEvent(new UserStatusChangedDomainEvent(Id, Status, LastTimeOnline));
        }
    }
}
