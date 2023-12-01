
namespace Choice.Application.UseCases.Clients.GetClients
{
    public interface IGetClientsUseCase
    {
        Task Execute();

        void SetOutputPort(IOutputPort outputPort);
    }
}
