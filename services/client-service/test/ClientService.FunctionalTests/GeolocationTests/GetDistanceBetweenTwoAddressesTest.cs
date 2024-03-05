﻿using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Infrastructure.Geolocation;
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

            Address address1 = new("Арбат 26", "Москва");
            Address address2 = new("Ангарская 21", "Москва");

            int distance = await addressService.GetDistance(address1, address2);

            Assert.Equal(20616, distance);
        }
    }
}