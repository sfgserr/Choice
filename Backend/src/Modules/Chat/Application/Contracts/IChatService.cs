using Chat.Application.Messages.Queries.GetChat;
using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Application.Contracts
{
    public interface IChatService 
    {
        Task SendMessage(MessageDto message);

        Task SendOrder(OrderResponseDto response, Guid toUserId);
    }
}
