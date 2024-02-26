
namespace Choice.Ordering.Application.UseCases.SetOrderStatus
{
    public interface ISetOrderStatusUseCase
    {
        Task Execute(int orderId);

        void SetOutputPort(IOutputPort outputPort);
    }
}
