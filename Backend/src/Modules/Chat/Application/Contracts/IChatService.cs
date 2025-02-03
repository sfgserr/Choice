using Chat.Application.Chat.Commands.SendMessageCommand;
using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Application.Contracts
{
    public interface IChatService 
    {
        Task SendMessage(MessageDto message);

        Task SendOrder(object data, Guid toUserId);

        Task SendMessageRead(Guid toUserId, Guid messageId);
    }
}
