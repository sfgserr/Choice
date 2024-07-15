using FirebaseAdmin.Messaging;

namespace Chat.Api.Services
{
    public class NotificationService
    {
        public static async Task SendNotificationAsync(string title, string body, List<string> tokens)
        {
            foreach (string deviceToken in tokens)
            {
                Message notification = new()
                {
                    Notification = new Notification()
                    {
                        Title = title,
                        Body = body,
                        ImageUrl = "http://87.228.9.225/api/objects/choice-logo-png"
                    },
                    Token = deviceToken,
                };

                await FirebaseMessaging.DefaultInstance.SendAsync(notification);
            }
        }
    }
}
