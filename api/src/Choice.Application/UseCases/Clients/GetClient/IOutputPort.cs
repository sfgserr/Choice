using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClient
{
    public interface IOutputPort
    {
        void Ok(Client client);

        void NotFound();
    }
}
