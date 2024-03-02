using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeUserData
{
    public interface IOutputPort
    {
        void Ok(Client client);

        void NotFound();

        void Invalid();
    }
}
