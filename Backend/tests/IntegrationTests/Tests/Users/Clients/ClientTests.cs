using IntegrationTests.SeedWork;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.Users.Clients
{
    public class ClientTests : Sut
    {
        public ClientTests(
            ITestOutputHelper outputHelper, 
            Fixture fixture) : base(outputHelper, fixture)
        {
            
        }

        [Fact]
        public async Task CreateClientReturnsOk()
        {
            var result = await ExecuteTest(async factory => 
            {
                var client = factory.CreateClient();

                var response = await client.PostAsync(
                    "api/clients",
                    JsonContent.Create(new
                    {
                        Name = "string",
                        Email = "string",
                        Password = "string",
                        PhoneNumber = "string",
                        City = "string",
                        Street = "string"
                    }));

                return new CreateClientProbe(response.IsSuccessStatusCode);
            });

            Assert.True(result);
        }

        private class CreateClientProbe : IProbe
        {
            private readonly bool _isSuccessStatusCode;

            public CreateClientProbe(bool isSuccessStatusCode)
            {
                _isSuccessStatusCode = isSuccessStatusCode;
            }
            
            public bool Test() => _isSuccessStatusCode;
        }
    }
}
