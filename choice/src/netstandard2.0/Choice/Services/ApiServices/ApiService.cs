using Choice.Services.HttpClientServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.ApiServices
{
    public class ApiService<T> : IApiService<T> where T : class
    {
        private readonly IHttpClientService<T> _clientService;
        private readonly string _baseUri;

        public ApiService(IHttpClientService<T> clientService, string baseUri)
        {
            _clientService = clientService;
            _baseUri = baseUri;
        }

        public async Task Delete(string requestUri, T body)
        {
            await _clientService.Delete($"{_baseUri}/{requestUri}", body);
        }

        public async Task<T> Get(string requestUri)
        {
            return await _clientService.Get($"{_baseUri}/{requestUri}");
        }

        public async Task<IList<T>> GetAll(string requestUri)
        {
            return await _clientService.GetAll($"{_baseUri}/{requestUri}");
        }

        public async Task<T> Post(string requestUri, T body)
        {
            return await _clientService.Post($"{_baseUri}/{requestUri}", body);
        }

        public async Task<T> Put(string requestUri, T body)
        {
            return await _clientService.Put($"{_baseUri}/{requestUri}", body);
        }
    }
}
