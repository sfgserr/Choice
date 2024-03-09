using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClient
{
    public interface IOutputPort
    {
        void Ok(Client client);

        void NotFound();
    }
}
