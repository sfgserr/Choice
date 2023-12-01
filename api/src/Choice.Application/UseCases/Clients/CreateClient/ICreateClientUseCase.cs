
namespace Choice.Application.UseCases.Clients.CreateClient
{
    public interface ICreateClientUseCase
    {
        Task Execute(string name, string surname, string password, string email, string photoUri);

        void SetOutputPort(IOutputPort outputPort);
    }
}
