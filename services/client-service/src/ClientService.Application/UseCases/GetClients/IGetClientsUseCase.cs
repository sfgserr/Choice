
namespace Choice.ClientService.Application.UseCases.GetClients
{
    public interface IGetClientsUseCase
    {
        Task Execute();

        void SetOutputPort(IOutputPort outputPort);
    }
}
