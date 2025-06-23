namespace Chat.Application.RealTimeMessaging
{
    public class Notification
    {
        public Notification(Guid userId, string title, string body)
        {
            UserId = userId;
            Title = title;
            Body = body;
        }
        
        public Guid UserId { get; }
        
        public string Title { get; }
        
        public string Body { get; }
    }
}