using Chat.Application.Contracts;
using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using WebApi.Modules;

namespace WebApi.Configuration.Chat
{
    [HasPermission(Permissions.Chat)]
    public class ChatHub : Hub
    {
        private readonly IChatUsersStore _usersStore;
        
        public ChatHub(IChatUsersStore usersStore)
        {
            _usersStore = usersStore;
        }

        public override Task OnConnectedAsync()
        {
            _usersStore.Connect(Guid.Parse(Context.UserIdentifier!), Context.ConnectionId);
            
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _usersStore.Disconnect(Guid.Parse(Context.UserIdentifier!), Context.ConnectionId);

            if (exception != null)
            {
                Log.Error(exception.Message, "Disconnected from server");
            }
            
            return Task.CompletedTask;
        }
    }
}