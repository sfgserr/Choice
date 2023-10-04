using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.UpdateClient
{
    public interface IUpdateClientUseCase
    {
        Task Execute(Client client);

        void SetOutputPort(IOutputPort outputPort);
    }
}
