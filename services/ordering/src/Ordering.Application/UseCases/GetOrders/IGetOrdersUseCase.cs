namespace Choice.Ordering.Application.UseCases.GetOrders
{
    public interface IGetOrdersUseCase
    {
        Task Execute(string guid);

        void SetOutputPort(IOutputPort outputPort);
    }
}
