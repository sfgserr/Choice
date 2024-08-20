using BuildingBlocks.Domain;

namespace Chat.Domain.ChatUsers.Events
{
    public class UserStatusChangedDomainEvent : DomainEventBase
    {
        public UserStatusChangedDomainEvent(ChatUserId userId, UserStatus userStatus, DateTime? lastTimeOnline)
        {
            UserId = userId;
            UserStatus = userStatus;
            LastTimeOnline = lastTimeOnline;
        }

        public ChatUserId UserId { get; }

        public UserStatus UserStatus { get; }

        public DateTime? LastTimeOnline { get; }
    }
}
