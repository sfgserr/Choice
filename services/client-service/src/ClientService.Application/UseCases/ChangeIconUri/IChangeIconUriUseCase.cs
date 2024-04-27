
namespace Choice.ClientService.Application.UseCases.ChangeIconUri
{
    public interface IChangeIconUriUseCase
    {
        Task Execute(string iconUri);

        void SetOutputPort(IOutputPort outputPort);
    }
}
