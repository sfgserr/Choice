using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.GeoCoding;
using Newtonsoft.Json.Linq;

namespace BuildingBlocks.Infrastructure.GeoCoding
{
    public class GeoCodingService : IGeoCodingService
    {
        private readonly IHttpClientFactory _factory;
        private readonly string _apiKey;

        public GeoCodingService(IHttpClientFactory factory, string apiKey)
        {
            _factory = factory;
            _apiKey = apiKey;
        }

        public async Task<string[]> GetCoords(string city, string street)
        {
            var client = _factory.CreateClient("Geocode");

            var response = await client.GetAsync($"/1.x/?apikey={_apiKey}&geocode={city},{street}&format=json");

            if (!response.IsSuccessStatusCode)
                throw new InvalidCommandException(["Error getting coords"]);

            var json = await response.Content.ReadAsStringAsync();

            return JObject.Parse(json)
                .SelectToken("response.GeoObjectCollection.featureMember[0].GeoObject.Point.pos")!
                .Value<string>()!
                .Split(' ');
        }
    }
}