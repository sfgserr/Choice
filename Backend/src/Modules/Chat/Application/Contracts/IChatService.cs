
namespace Chat.Application.Contracts
{
    public interface IChatService 
    {
        Task Send(object data, Guid toUserId);
    }
}
