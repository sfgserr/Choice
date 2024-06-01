using Choice.Application.Services;
using Choice.Common.ValueObjects;
using Newtonsoft.Json.Linq;

namespace Choice.Infrastructure.Geolocation
{
    public class AddressService : IAddressService
    {
        private const string _geocodeUrl = "https://api.geoapify.com/v1";

        private readonly HttpClient _httpClient;
        private readonly AddressServiceOptions _options;

        public AddressService(HttpClient httpClient, AddressServiceOptions options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<int> GetDistance(string clientCoords, string companyCoords)
        {
            Uri requestUri = new($"{_geocodeUrl}/routing?waypoints={clientCoords}|{companyCoords}&mode=drive&apiKey={_options.ApiKey}");

            HttpResponseMessage response = await _httpClient
                .GetAsync(requestUri);

            string json = await response.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(json);

            return int.Parse(obj?.SelectToken("features[0].properties.distance")?.ToString()!);
        }

        public async Task<string> Geocode(Address address)
        {
            Uri requestUri = new
                ($"{_geocodeUrl}/geocode/search?text={address.Street},{address.City}&apiKey={_options.ApiKey}");

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            string json = await response.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(json);

            return $"{obj?.SelectToken("features[0].properties.lat")?.Value<string>()},{obj?.SelectToken("features[0].properties.lon")?.Value<string>()}";
        }
    }
}
