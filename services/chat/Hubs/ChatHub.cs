using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
    }
}
