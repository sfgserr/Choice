using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.UpdateOrderMessage
{
    public interface IUpdateOrderMessageUseCase
    {
        Task Execute(OrderMessage orderMessage);

        void SetOutputPort(IOutputPort outputPort);
    }
}
