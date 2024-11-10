namespace Chat.Application.Messages.Queries.GetChats
{
    public class ChatDto
    {
        public Guid UserId { get; }

        public string IconUri { get; }

        public string UserName { get; }

        public string LastMessage { get; }

        public Guid LastMessageId { get; }

        public DateTime LastMessageCreationDate { get; }

        public bool IsOnline { get; set; }
    }
}
