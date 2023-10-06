using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.GetOrder
{
    public interface IGetOrderUseCase
    {
        Task Execute(Func<Order, bool> func);

        void SetOutputPort(IOutputPort outputPort);
    }
}
