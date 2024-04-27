using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeIconUri
{
    public interface IOutputPort
    {
        void Ok(Client client);

        void NotFound();

        void Invalid();
    }
}
