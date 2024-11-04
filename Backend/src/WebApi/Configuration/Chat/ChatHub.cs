using BuildingBlocks.Application.Authentication;
using Chat.Application.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Configuration.Chat
{
    public class ChatHub : Hub
    {
        private readonly IChatUsersStore _usersStore;
        private readonly IUserService _userService;
        
        public ChatHub(IChatUsersStore usersStore, IUserService userService)
        {
            _usersStore = usersStore;
            _userService = userService;
        }

        public override Task OnConnectedAsync()
        {
            _usersStore.Connect(_userService.GetUserId(), Context.ConnectionId);
            
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _usersStore.Disconnect(_userService.GetUserId());
            
            return Task.CompletedTask;
        }
    }
}