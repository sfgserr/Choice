using BuildingBlocks.Domain;

namespace Users.Domain.Users.Clients
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> Get(ClientId id);
    }
}
