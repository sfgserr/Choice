using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.UpdateClient
{
    public interface IOutputPort
    {
        void Ok(Client client);
    }
}
