using IntegrationTests.SeedWork;
using IntegrationTests.SeedWork.Probes;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.Users
{
    public class ClientTests : Sut
    {
        public ClientTests(
            ITestOutputHelper outputHelper, 
            Fixture fixture) : base(outputHelper, fixture)
        {
            
        }

        //[Fact]
        public async Task GetClientTest()
        {
            var result = await ExecuteAuthorizedTest(async (factory, token) => 
            {
                var client = factory.CreateClient();

                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "api/clients");

                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);

                return new CheckSuccessStatusCodeProbe(response.IsSuccessStatusCode);
            }, 0, true);

            Assert.True(result); 
        }
    }
}
