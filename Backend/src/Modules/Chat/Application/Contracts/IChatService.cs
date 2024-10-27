using Chat.Domain.Messages;

namespace Chat.Application.Contracts
{
    public interface IChatService 
    {
        bool IsUserOnline(Guid userId);

        Task SendMessage(Message message);
    }
}
