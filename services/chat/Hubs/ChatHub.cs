using Choice.Chat.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        public override Task OnConnectedAsync()
        {
            string id = Context.User?.FindFirst("id")?.Value!;

            _chatService.Add(id, Context.ConnectionId);
            
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string id = Context.User?.FindFirst("id")?.Value!;

            _chatService.Remove(id);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
