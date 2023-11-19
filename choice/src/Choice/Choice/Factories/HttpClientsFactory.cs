using System.Collections.Generic;
using System.Net.Http;

namespace Choice.Factories
{
    public class HttpClientsFactory : IHttpClientsFactory
    {   
        private readonly Dictionary<string, HttpClient> _clients = new Dictionary<string, HttpClient>();

        public HttpClientsFactory()
        {
            Initialize();
        }

        public void Initialize()
        {
            CreateClient("Api");
            CreateClient("Address");
        }

        public void CreateClient(string name)
        {
            if (_clients.ContainsKey(name))
                return;

            _clients.Add(name, new HttpClient());
        }

        public HttpClient GetClient(string name)
        {
            return _clients.ContainsKey(name) ? _clients[name] : null;
        }
    }
}
