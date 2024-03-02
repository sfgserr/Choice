using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Domain.ClientAggregate
{
    public interface IClientRepository
    {
        Task Add(Client client);

        Task<Client> Get(int id);

        Task<IList<Client>> GetAll();

        Task<IList<OrderRequest>> GetRequests();

        void Update(Client client);

        Task Update(OrderRequest request);
    }
}
