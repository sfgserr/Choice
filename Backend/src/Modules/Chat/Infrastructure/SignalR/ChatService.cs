using Chat.Application.Chat.Commands.SendMessageCommand;
using Chat.Application.Contracts;
using Microsoft.AspNetCore.SignalR;
using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Infrastructure.SignalR
{
    public class ChatService<T> : IChatService where T : Hub
    {
        private readonly IChatUsersStore _usersStore;
        private readonly IHubContext<T> _hubContext;

        public ChatService(IChatUsersStore usersStore, IHubContext<T> hubContext)
        {
            _usersStore = usersStore;
            _hubContext = hubContext;
        }

        public async Task SendMessage(MessageDto message)
        {
            var toUserId = message.ToUserId;

            if (_usersStore.IsUserOnline(toUserId))
            {
                await _hubContext.Clients
                    .User(_usersStore.GetConnectionId(toUserId))
                    .SendAsync("messageSent", message);
            }
        }

        public async Task SendOrder(object data, Guid toUserId)
        {
            if (_usersStore.IsUserOnline(toUserId))
            {
                await _hubContext.Clients
                    .User(_usersStore.GetConnectionId(toUserId))
                    .SendAsync("orderSent", data);
            }
        }

        public async Task SendMessageRead(Guid toUserId, Guid messageId)
        {
            if (_usersStore.IsUserOnline(toUserId))
            {
                await _hubContext.Clients
                    .User(_usersStore.GetConnectionId(toUserId))
                    .SendAsync("messageRead", messageId);
            }
        }
    }
}