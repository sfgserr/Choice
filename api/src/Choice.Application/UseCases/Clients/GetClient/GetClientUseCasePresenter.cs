using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClient
{
    public class GetClientUseCasePresenter : IOutputPort
    {
        public bool IsNotFound { get; set; } = false;
        public Client Client { get; set; }

        public void NotFound()
        {
            IsNotFound = true;
        }

        public void Ok(Client client)
        {
            Client = client;
        }
    }
}
