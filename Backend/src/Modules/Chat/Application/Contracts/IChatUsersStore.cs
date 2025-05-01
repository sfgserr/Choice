namespace Chat.Application.Contracts
{
    public interface IChatUsersStore
    {
        List<string>? GetConnectionsId(Guid userId);
        
        bool IsUserOnline(Guid userId);

        void Connect(Guid id, string connectionId);

        void Disconnect(Guid id, string connectionId);
    }
}