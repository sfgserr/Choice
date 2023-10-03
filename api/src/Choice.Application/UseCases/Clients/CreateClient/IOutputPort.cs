using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.CreateClient
{
    public interface IOutputPort
    {
        void Ok(Client client);

        void Invalid();
    }
}
