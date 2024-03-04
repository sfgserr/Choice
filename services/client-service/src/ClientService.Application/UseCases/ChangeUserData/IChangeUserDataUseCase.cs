
namespace Choice.ClientService.Application.UseCases.ChangeUserData
{
    public interface IChangeUserDataUseCase
    {
        Task Execute(string name, string surname);

        void SetOutputPort(IOutputPort outputPort);
    }
}
