
namespace Choice.ClientService.Application.UseCases.GetClientAdmin
{
    public interface IGetClientAdminUseCase
    {
        void SetOutputPort(IOutputPort outputPort);

        Task Execute(string id);
    }
}
