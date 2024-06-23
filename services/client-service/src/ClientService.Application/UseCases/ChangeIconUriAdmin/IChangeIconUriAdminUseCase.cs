
namespace Choice.ClientService.Application.UseCases.ChangeIconUriAdmin
{
    public interface IChangeIconUriAdminUseCase
    {
        Task Execute(string id, string iconUri);

        void SetOutputPort(IOutputPort outputPort);
    }
}
