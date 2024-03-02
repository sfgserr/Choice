
namespace Choice.ClientService.Application.UseCases.GetOrderRequests
{
    public interface IGetOrderRequestsUseCase
    {
        Task Execute();

        void SetOutputPort(IOutputPort outputPort);
    }
}
