
namespace Choice.ClientService.Application.UseCases.ChangeUserData
{
    public interface IChangeUserDataUseCase
    {
        Task Execute(int clientId, string name, string surname);

        void SetOutputPort(IOutputPort outputPort);
    }
}
