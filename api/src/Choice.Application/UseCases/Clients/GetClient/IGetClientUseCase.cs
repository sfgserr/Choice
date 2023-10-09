using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClient
{
    public interface IGetClientUseCase
    {
        Task Execute(int id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
