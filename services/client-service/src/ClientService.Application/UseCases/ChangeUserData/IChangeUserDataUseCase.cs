
namespace Choice.ClientService.Application.UseCases.ChangeUserData
{
    public interface IChangeUserDataUseCase
    {
        Task Execute(string name, string surname, string email, string phoneNumber);

        void SetOutputPort(IOutputPort outputPort);
    }
}
