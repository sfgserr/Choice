using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;

namespace IntegrationTests.Users.Users.CreateUser
{
    public class CreateUserTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CreateUserTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateUserReturnsOk()
        {
            var client = _factory.CreateClient();

            var result = await client.PostAsync(
                "/api/clients",
                JsonContent.Create(new 
                {
                    Name = "string",
                    Email = "string",
                    Password = "string",
                    PhoneNumber = "string",
                    City = "string",
                    Street = "string"
                }));

            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
