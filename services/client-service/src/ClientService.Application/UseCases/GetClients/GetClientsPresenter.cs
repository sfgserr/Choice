using Choice.ClientService.Application.UseCases.CreateClient;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClients
{
    public sealed class GetClientsPresenter : IOutputPort
    {
        public IList<Client>? Clients { get; set; }
        public bool IsInvalid { get; set; } 

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void Ok(IList<Client> clients)
        {
            Clients = clients;    
        }
    }
}
