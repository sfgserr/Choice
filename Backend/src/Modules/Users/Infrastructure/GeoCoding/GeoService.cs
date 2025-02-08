using System.Globalization;
using BuildingBlocks.Application.Exceptions;
using Newtonsoft.Json.Linq;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Infrastructure.GeoCoding
{
    internal class GeoService : IGeoService
    {
        private const double EarthRadius = 6.371e6;

        private readonly IHttpClientFactory _factory;
        private readonly string _apiKey;
        
        internal GeoService(IHttpClientFactory factory, string apiKey)
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

        public int GetDistance(Coords coords1, Coords coords2)
        {
            double lat1 = Parse(coords1.Latitude) * (Math.PI / 180);
            double lat2 = Parse(coords2.Latitude) * (Math.PI / 180);
            double lon1 = Parse(coords1.Longitude) * (Math.PI / 180);
            double lon2 = Parse(coords1.Longitude) * (Math.PI / 180);

            double a = Math.Pow(Math.Sin((lat1 - lat2) / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin((lon1 - lon2) / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));

            return (int)(EarthRadius * c);
        }

        private double Parse(string s)
        {
            return double.Parse(s, CultureInfo.InvariantCulture);
        }
    }
}
