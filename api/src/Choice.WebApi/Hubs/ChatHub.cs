using Choice.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace Choice.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendChatMessageAsync(int roomId, string message)
        {
            await Clients.All.SendAsync("Receive", roomId, message);
        }

        public async Task SendOrderMessageAsync(OrderMessage orderMessage)
        {
            await Clients.All.SendAsync("Receive", orderMessage.Room.Id, orderMessage);
        }
    }
}
