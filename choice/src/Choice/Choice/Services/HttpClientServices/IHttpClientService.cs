using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.HttpClientServices
{
    public interface IHttpClientService<T>
    {
        Task<T> Post(string uri, T body);

        Task<IList<T>> GetAll(string uri);

        Task<T> Get(string uri);

        Task<T> Put(string uri, T body);

        Task Delete(string uri, T body);
    }
}
