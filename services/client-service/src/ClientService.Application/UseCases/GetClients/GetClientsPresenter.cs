using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClients
{
    public sealed class GetClientsPresenter : IOutputPort
    {
        public IList<Client>? Clients { get; set; }

        public void Ok(IList<Client> clients)
        {
            Clients = clients;    
        }
    }
}
