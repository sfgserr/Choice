using Microsoft.AspNetCore.SignalR;

namespace WebApi.Configuration.Chat
{
    public sealed class SubjectBasedUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.FindFirst(c => c.Type == "sub")?.Value!;
        }
    }
}