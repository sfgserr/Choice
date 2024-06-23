using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClientAdmin
{
    public interface IOutputPort
    {
        void Ok(Client client);

        void NotFound();
    }
}
