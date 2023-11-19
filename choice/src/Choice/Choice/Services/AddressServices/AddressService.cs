using Choice.Factories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Choice.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly IHttpClientsFactory _factory;

        public AddressService(IHttpClientsFactory factory)
        {
            _factory = factory;
        }

        public async Task<string> GetAddressByCoordinates(double latitude, double longtitude)
        {
            string uri = $"http://nominatim.openstreetmap.org/reverse?format=json&lat={latitude}&lon={longtitude}";

            HttpClient client = _factory.GetClient("Address");

            string json = await client.GetStringAsync(uri);

            JObject jObject = JObject.Parse(json);

            string address = jObject.GetValue("display_name").ToString();

            return address;
        }
    }
}
