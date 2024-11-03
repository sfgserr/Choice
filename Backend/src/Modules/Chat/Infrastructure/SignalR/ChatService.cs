using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;
using Chat.Domain.Messages;
using Microsoft.AspNetCore.SignalR;
using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Infrastructure.SignalR
{
    internal class ChatService : Hub, IChatService
    {
        private readonly Dictionary<Guid, string> _users;
        private readonly IUserContext _userContext;

        internal ChatService(IUserContext userContext)
        {
            _users = new();
            _userContext = userContext;
        }

        public override Task OnConnectedAsync()
        {
            _users.Add(_userContext.Id.Value, Context.ConnectionId);

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _users.Remove(_userContext.Id.Value);

            return Task.CompletedTask;
        }

        public bool IsUserOnline(Guid userId)
        {
            return _users.ContainsKey(userId);
        }

        public async Task SendMessage(Message message)
        {
            var toUserId = message.ToUserId.Value;

            if (IsUserOnline(toUserId))
                await Clients.User(_users[toUserId]).SendAsync("messageSent", message);
        }

        public async Task SendOrder(OrderResponseDto response, Guid toUserId)
        {
            if (IsUserOnline(toUserId))
                await Clients.User(_users[toUserId]).SendAsync("orderSent", response);
        }
    }
}