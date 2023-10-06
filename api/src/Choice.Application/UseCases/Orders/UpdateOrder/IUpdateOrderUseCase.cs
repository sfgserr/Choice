using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.UpdateOrder
{
    public interface IUpdateOrderUseCase
    {
        Task Execute(Order order);

        void SetOutputPort(IOutputPort outputPort);
    }
}
