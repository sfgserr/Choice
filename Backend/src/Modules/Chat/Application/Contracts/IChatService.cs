using Chat.Domain.Messages;
using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Application.Contracts
{
    public interface IChatService 
    {
        Task SendMessage(Message message);

        Task SendOrder(OrderResponseDto response, Guid toUserId);
    }
}
