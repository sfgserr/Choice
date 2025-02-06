namespace Chat.Application.Messages.Queries.GetChats
{
    public class ChatDto
    {
        public Guid UserId { get; }

        public string IconUri { get; }

        public string UserName { get; }
        
        public Guid LastMessageId { get; }
        
        public Guid LastMessageUserSenderId { get; }
        
        public string? LastMessage { get; }
        
        public string LastMessageIsRead { get; }
        
        public DateTime LastMessageCreationDate { get; }
    }
}
