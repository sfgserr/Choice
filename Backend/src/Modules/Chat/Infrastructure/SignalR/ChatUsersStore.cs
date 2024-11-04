using Chat.Application.Contracts;

namespace Chat.Infrastructure.SignalR
{
    public class ChatUsersStore : IChatUsersStore
    {
        private readonly Dictionary<Guid, string> _users = new();

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
            _users.Add(id, connectionId);
        }

        public void Disconnect(Guid id)
        {
            _users.Remove(id);
        }
    }
}