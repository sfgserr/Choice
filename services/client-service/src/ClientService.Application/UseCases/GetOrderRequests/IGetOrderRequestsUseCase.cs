
namespace Choice.ClientService.Application.UseCases.GetOrderRequests
{
    public interface IGetOrderRequestsUseCase
    {
        Task Execute(int[] categoriesId);

        void SetOutputPort(IOutputPort outputPort);
    }
}
