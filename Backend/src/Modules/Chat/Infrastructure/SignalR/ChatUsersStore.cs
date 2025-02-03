using System.Collections.Concurrent;
using Chat.Application.Contracts;

namespace Chat.Infrastructure.SignalR
{
    public class ChatUsersStore : IChatUsersStore
    {
        private readonly ConcurrentDictionary<Guid, string> _users = new();

        public string GetConnectionId(Guid id)
        {
            return _users[id];
        }
        
        public bool IsUserOnline(Guid userId)
        {
            return _users.ContainsKey(userId);
        }

        public void Connect(Guid id, string connectionId)
        {
            _users.TryAdd(id, connectionId);
        }

        public void Disconnect(Guid id)
        {
            _users.Remove(id, out _);
        }
    }
}