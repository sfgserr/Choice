namespace WebApi.Modules.Chat.Messages
{
    public class CreateMessageRequest
    {
        public CreateMessageRequest(string content, Guid toUserId, string type)
        {
            Content = content;
            ToUserId = toUserId;
            Type = type;
        }

        public string Content { get; }

        public Guid ToUserId { get; }

        public string Type { get; }
    }
}