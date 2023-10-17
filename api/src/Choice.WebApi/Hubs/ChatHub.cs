using Microsoft.AspNetCore.SignalR;

namespace Choice.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendAsync(string roomName, string message)
        {
            await Clients.All.SendAsync("Receive", roomName, message);
        }
    }
}
