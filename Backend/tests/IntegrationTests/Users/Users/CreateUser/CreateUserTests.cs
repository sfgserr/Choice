using System.Text.Json;

namespace IntegrationTests.Users.Users.CreateUser
{
    public class CreateUserTests : IClassFixture<ChoiceWebApplicationFactory>
    {
        private readonly ChoiceWebApplicationFactory _factory;

        public CreateUserTests(ChoiceWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateUserReturnsOk()
        {
            var client = _factory.CreateClient();

            var result = await client.PostAsync(
                "/api/users",
                new StringContent(JsonSerializer.Serialize(new 
                {
                    Name = "string",
                    Email = "string",
                    Password = "string",
                    PhoneNumber = "string",
                    City = "string",
                    Street = "string"
                })));

            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
