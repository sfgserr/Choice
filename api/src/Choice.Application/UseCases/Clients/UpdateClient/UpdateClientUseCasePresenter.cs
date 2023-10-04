using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.UpdateClient
{
    public class UpdateClientUseCasePresenter : IOutputPort
    {
        public Client Client { get; set; }

        public void Ok(Client client)
        {
            Client = client;
        }
    }
}
