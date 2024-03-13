namespace Choice.Ordering.Application.UseCases.GetOrders
{
    public interface IGetOrdersUseCase
    {
        Task Execute();

        void SetOutputPort(IOutputPort outputPort);
    }
}
