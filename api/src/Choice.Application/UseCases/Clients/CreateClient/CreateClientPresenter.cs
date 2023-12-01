using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.CreateClient
{
    public class CreateClientPresenter : IOutputPort
    {
        public Client? Client { get; set; }
        public bool IsInvalid { get; set; } = false;

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void Ok(Client client)
        {
            Client = client;
        }
    }
}
