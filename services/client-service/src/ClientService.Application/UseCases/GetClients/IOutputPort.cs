using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClients
{
    public interface IOutputPort
    {
        void Ok(IList<Client> clients);
    }
}
