using Chat.Application.Contracts;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Chat.Infrastructure.SignalR
{
    public class ChatService<T> : IChatService where T : Hub
    {
        private readonly IChatUsersStore _usersStore;
        private readonly IHubContext<T> _hubContext;
        private readonly ILogger _logger;
        
        public ChatService(IChatUsersStore usersStore, IHubContext<T> hubContext, ILogger logger)
        {
            _usersStore = usersStore;
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Send(object message, Guid toUserId, string methodName)
        {
            _logger.Information("Sending message to {ToUserId}", toUserId);

            if (_usersStore.IsUserOnline(toUserId))
            {
                var connectionId = _usersStore.GetConnectionId(toUserId);
                
                _logger.Information("Start sending message to connectionId {0}", connectionId);
                
                await _hubContext.Clients
                    .Client(connectionId)
                    .SendAsync(methodName, message);
            }
            else
            {
                _logger.Information("User is not online");
            }
        }
    }
}