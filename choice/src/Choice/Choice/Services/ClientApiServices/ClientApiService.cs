using Choice.Domain.Models;
using Choice.Services.ApiServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.ClientApiServices
{
    public class ClientApiService : IClientApiService
    {
        private readonly IApiService<Client> _clientService;

        public ClientApiService(IApiService<Client> clientService)
        {
            _clientService = clientService;
        }

        public async Task<Client> Post(Client client)
        {
            return await _clientService.Post("Client/Create", client);
        }

        public async Task<IList<Client>> GetAll()
        {
            return await _clientService.GetAll("Client/Get");
        }

        public async Task<Client> Get(int id)
        {
            return await _clientService.Get($"Client/{id}/Get");
        }

        public async Task<Client> GetByEmail(string email)
        {
            return await _clientService.Get($"Client/GetByEmail?email={email}");
        }

        public async Task<Client> Put(Client client)
        {
            return await _clientService.Put("Client/Update", client);
        }
    }
}
