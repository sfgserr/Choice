namespace Chat.Application.Messages.Queries.GetChats
{
    public class ChatDto
    {
        public ChatDto(
            Guid userId, 
            string iconUri, 
            string userName, 
            string lastMessage, 
            Guid lastMessageId, 
            DateTime lastMessageCreationDate)
        {
            UserId = userId;
            IconUri = iconUri;
            UserName = userName;
            LastMessage = lastMessage;
            LastMessageId = lastMessageId;
            LastMessageCreationDate = lastMessageCreationDate;
        }

        public Guid UserId { get; }

        public string IconUri { get; }

        public string UserName { get; }

        public string LastMessage { get; }

        public Guid LastMessageId { get; }

        public DateTime LastMessageCreationDate { get; }

        public bool IsOnline { get; set; }
    }
}
