using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClients
{
    public interface IOutputPort
    {
        void Ok(IList<Client> clients);
    }
}
