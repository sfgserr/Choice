using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        private readonly IUserRepository _repository;

        public ChatHub(ChatService chatService, IUserRepository repository)
        {
            _chatService = chatService;
            _repository = repository;
        }

        public override async Task OnConnectedAsync()
        {
            string id = Context.User?.FindFirst("id")?.Value!;

            _chatService.Add(id, Context.ConnectionId);

            User user = (await _repository.Get(id))!;

            user.SetStatus(UserStatus.Online);

            await _repository.Update(user);

            await Clients.All.SendAsync("statusChanged", user);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string id = Context.User?.FindFirst("id")?.Value!;

            _chatService.Remove(id);

            User user = (await _repository.Get(id))!;

            user.SetStatus(UserStatus.Offline);

            await _repository.Update(user);

            await Clients.All.SendAsync("statusChanged", user);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
