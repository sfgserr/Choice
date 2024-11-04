namespace Chat.Application.Contracts
{
    public interface IChatUsersStore
    {
        string GetConnectionId(Guid userId);
        
        bool IsUserOnline(Guid userId);

        void Connect(Guid id, string connectionId);

        void Disconnect(Guid id);
    }
}