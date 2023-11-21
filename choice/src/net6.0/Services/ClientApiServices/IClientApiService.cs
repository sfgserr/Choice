
using Choice.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.ClientApiServices
{
    public interface IClientApiService
    {
        Task<Client> Post(Client client);

        Task<IList<Client>> GetAll();

        Task<Client> Get(int id);

        Task<Client> GetByEmail(string email);

        Task<Client> Put(Client client);
    }
}
