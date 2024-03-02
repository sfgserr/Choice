using Choice.ClientService.Application.UseCases.CreateClient;

namespace Choice.ClientService.Application.UseCases.GetClients
{
    public interface IGetClientsUseCase
    {
        Task Execute();

        void SetOutputPort(IOutputPort outputPort);
    }
}
