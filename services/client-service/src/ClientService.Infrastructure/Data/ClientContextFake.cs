using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;
using Choice.Infrastructure.Data;

namespace Choice.ClientService.Infrastructure.Data
{
    public sealed class ClientContextFake
    {
        public ClientContextFake()
        {
            Clients.Add(new Client
                (SeedData.DefaultClientGuid, "Name", "Surname", "Email", new("Street", "City"), "FakeUri", "FakeNumber"));
        }

        public List<Client> Clients { get; } = new();
        public List<OrderRequest> Requests { get; } = new();
    }
}
