using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClientByEmail
{
    public interface IOutputPort
    {
        void Ok(Client client);

        void NotFound();
    }
}
