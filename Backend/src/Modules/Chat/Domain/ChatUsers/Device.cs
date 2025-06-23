using BuildingBlocks.Domain;

namespace Chat.Domain.ChatUsers
{
    public class Device : Entity
    {
        private string _token;
        
        private DateTime _expirationDate;
        
        private Device()
        {
            
        }

        private Device(ChatUserId userId, string name, string token, DateTime expirationDate)
        {
            UserId = userId;
            Name = name;
            
            _token = token;
            _expirationDate = expirationDate;
        }

        public ChatUserId UserId { get; }
        
        public string Name { get; }

        internal static Device Create(ChatUserId userId, string deviceName, string token)
        {
            return new Device(
                userId,
                deviceName,
                token,
                DateTime.UtcNow.AddDays(14));
        }

        internal void UpdateToken(string token)
        {
            _token = token;
            
            _expirationDate = DateTime.UtcNow.AddDays(14);
        }
    }
}