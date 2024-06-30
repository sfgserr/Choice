using Choice.Application.Services;
using Choice.Common.ValueObjects;
using Newtonsoft.Json.Linq;

namespace Choice.Infrastructure.Geolocation
{
    public class AddressService : IAddressService
    {
        private const string _googleApi = "https://maps.googleapis.com/maps/api";

        private readonly HttpClient _httpClient;
        private readonly AddressServiceOptions _options;

        public AddressService(HttpClient httpClient, AddressServiceOptions options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<int> GetDistance(Address clientAddress, Address companyAddress)
        {
            string clientAddressString = $"{clientAddress.City},{clientAddress.Street}";
            string companyAddressString = $"{companyAddress.City},{companyAddress.Street}";

            Uri requestUri = new($"{_googleApi}/distancematrix/json?destinations={clientAddressString}&origins={companyAddressString}&units=imperial&apiKey={_options.ApiKey}");

            HttpResponseMessage response = await _httpClient
                .GetAsync(requestUri);

            string json = await response.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(json);

            return int.Parse(obj?.SelectToken("rows[0].elements[0].distance.value")?.ToString()!);
        }

        public async Task<string> Geocode(Address address)
        {
            Uri requestUri = new
                ($"{_googleApi}/geocode/json?address={address.City},{address.Street}&apiKey={_options.ApiKey}");

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            string json = await response.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(json);

            return $"{obj?.SelectToken("results[0].geometry.location.lat")?.Value<string>()},{obj?.SelectToken("results[0].geometry.location.lng")?.Value<string>()}";
        }
    }
}
