using Choice.Common.ValueObjects;
using Choice.Infrastructure.Geolocation;
using Microsoft.Extensions.DependencyInjection;

namespace Choice.ClientService.IntegrationalTests.GeolocationTests
{
    public sealed class GetDistanceBetweenTwoAddressesTest
    {
        private const string _apiKey = "893c71be7dfe47b897e4f622951e11af";

        [Fact]
        public async Task GetDistance()
        {
            ServiceCollection services = new();

            services.AddSingleton<HttpClient>();
            services.AddSingleton<AddressService>(s =>
                new AddressService(s.GetRequiredService<HttpClient>(), new AddressServiceOptions(_apiKey)));

            ServiceProvider provider = services.BuildServiceProvider();

            AddressService addressService = provider.GetRequiredService<AddressService>();

            Address address1 = new(" Angarskaya 21 ", "Moscow");
            Address address2 = new("Арбат 26", "Москва");

            int distance = await addressService.GetDistance(address1, address2);

            Assert.Equal(5205, distance);
        }
    }
}
