using Choice.Factories;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Choice.Services.HttpClientServices
{
    public class HttpClientService<T> : IHttpClientService<T> where T : class
    {
        private readonly IHttpClientsFactory _factory;
        private readonly HttpClient _client;

        public HttpClientService(IHttpClientsFactory factory)
        {
            _factory = factory;
            _client = _factory.GetClient("Api");
        }

        public async Task Delete(string uri, T body)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri)
            {
                Content = JsonContent.Create(body)
            };

            await _client.SendAsync(request);
        }

        public async Task<T> Get(string uri)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> Get(string uri, Dictionary<string, string> headers)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            foreach (var header in headers) request.Headers.Add(header.Key, header.Value);

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<IList<T>> GetAll(string uri)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpResponseMessage response = await _client.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public async Task<T> Post(string uri, T body)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create(body)
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return null;

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> Post(string uri, T body, Dictionary<string, string> headers)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create(body)
            };
            foreach (var header in headers) request.Headers.Add(header.Key, header.Value);

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return null;

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> Put(string uri, T body)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = JsonContent.Create(body)
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> Put(string uri, T body, Dictionary<string, string> headers)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = JsonContent.Create(body)
            };
            foreach (var header in headers) request.Headers.Add(header.Key, header.Value);

            HttpResponseMessage response = await _client.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
