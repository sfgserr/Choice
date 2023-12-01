using System.Net.Http;

namespace Choice.Factories
{
    public interface IHttpClientsFactory
    {
        HttpClient GetClient(string name);

        void CreateClient(string name);
    }
}
