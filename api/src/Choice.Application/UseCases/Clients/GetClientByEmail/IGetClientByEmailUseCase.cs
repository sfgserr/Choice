
namespace Choice.Application.UseCases.Clients.GetClientByEmail
{
    public interface IGetClientByEmailUseCase
    {
        Task Execute(string email);

        void SetOutputPort(IOutputPort outputPort);
    }
}
