using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.ApiServices
{
    public interface IApiService<T>
    {
        Task<T> Post(string requestUri, T body);

        Task<IList<T>> GetAll(string requestUri);

        Task<T> Get(string requestUri);

        Task<T> Put(string requestUri, T body);

        Task Delete(string requestUri, T body);
    }
}
