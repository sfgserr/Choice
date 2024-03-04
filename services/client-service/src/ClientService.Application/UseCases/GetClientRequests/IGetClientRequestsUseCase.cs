
namespace Choice.ClientService.Application.UseCases.GetClientRequests
{
    public interface IGetClientRequestsUseCase
    {
        Task Execute();

        void SetOutputPort(IOutputPort outputPort);
    }
}
