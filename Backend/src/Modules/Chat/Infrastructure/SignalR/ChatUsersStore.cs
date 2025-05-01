using System.Collections.Concurrent;
using Chat.Application.Contracts;

namespace Chat.Infrastructure.SignalR
{
    public class ChatUsersStore : IChatUsersStore
    {
        private readonly ConcurrentDictionary<Guid, List<string>> _users = new();
        private readonly object _lock = new();
        
        public List<string>? GetConnectionsId(Guid id)
        {
            _users.TryGetValue(id, out var connectionsId);

            return connectionsId;
        }
        
        public bool IsUserOnline(Guid userId)
        {
            return _users.ContainsKey(userId);
        }

        public void Connect(Guid id, string connectionId)
        {
            _users.AddOrUpdate(
                id, 
                new List<string> { connectionId },
                (key, value) =>
                {
                    value.Add(connectionId);
                    
                    return value;
                });
        }

        public void Disconnect(Guid id, string connectionId)
        { 
            lock (_lock)
            {
                _users.TryGetValue(id, out var list);

                if (list != null)
                {
                    list.Remove(connectionId);

                    if (list.Count == 0)
                    {
                        _users.Remove(id, out _);
                    }
                }
            }
        }
    }
}