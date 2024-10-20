
namespace Chat.Application.Contracts
{
    public interface IChatService 
    {
        bool IsUserOnline(Guid userId);
    }
}
