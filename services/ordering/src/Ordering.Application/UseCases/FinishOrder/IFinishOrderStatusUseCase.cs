
namespace Choice.Ordering.Application.UseCases.FinishOrder
{
    public interface IFinishOrderStatusUseCase
    {
        Task Execute(int orderId);

        void SetOutputPort(IOutputPort outputPort);
    }
}
