using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.GetOrder
{
    public interface IGetOrderUseCase
    {
        Task Execute(int id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
