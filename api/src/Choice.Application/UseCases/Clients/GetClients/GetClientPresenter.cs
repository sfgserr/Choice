using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClients
{
    public class GetClientPresenter : IOutputPort
    {
        public IList<Client> Clients { get; set; } = new List<Client>();

        public void Ok(IList<Client> clients)
        {
            Clients = clients;
        }
    }
}
