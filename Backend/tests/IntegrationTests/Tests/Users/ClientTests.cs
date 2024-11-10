using IntegrationTests.SeedWork;
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
                var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "api/clients");

                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);

                return new TestResult(response.IsSuccessStatusCode);
            }, 0, true);

            Assert.True(result.IsSuccessful); 
        }
    }
}
