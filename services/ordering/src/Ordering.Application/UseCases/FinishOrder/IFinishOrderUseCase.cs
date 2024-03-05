
namespace Choice.Ordering.Application.UseCases.FinishOrder
{
    public interface IFinishOrderUseCase
    {
        Task Execute(int orderId);

        void SetOutputPort(IOutputPort outputPort);
    }
}
