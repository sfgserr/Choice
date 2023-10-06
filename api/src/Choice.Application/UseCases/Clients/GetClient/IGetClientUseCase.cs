using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClient
{
    public interface IGetClientUseCase
    {
        Task Execute(Func<Client, bool> func);

        void SetOutputPort(IOutputPort outputPort);
    }
}
