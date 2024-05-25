using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Services
{
    public sealed class ChatService
    {
        private readonly Dictionary<string, string> _connectionIds = [];

        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(string receiverGuid, string method, MessageViewModel message)
        {
            bool ifExist = _connectionIds.TryGetValue(receiverGuid, out string? connectionId);

            if (ifExist)
                await _hubContext.Clients.Client(connectionId!).SendAsync(method, message);
        }

        public void Add(string guid, string connectionId) =>
            _connectionIds.TryAdd(guid, connectionId);

        public void Remove(string guid) =>
            _connectionIds.Remove(guid);
    }
}
