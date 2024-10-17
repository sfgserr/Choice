namespace IntegrationTests.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _factory;

        private string? _clientToken;
        private string? _companyToken;

        public AuthService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<string?> GetToken(TokenType tokenType)
        {
            var token = tokenType == TokenType.Client ? _clientToken : _companyToken;

            if (token is null)
            {
                return tokenType switch
                {
                    TokenType.Client => _clientToken = await Login(tokenType),
                    TokenType.Company => _companyToken = await Login(tokenType),
                    _ => throw new ArgumentException()
                };
            }

            return token;
        }

        private async Task<string?> Login(TokenType tokenType)
        {
            var isTokenCreated = tokenType == TokenType.Client ? await CreateClient() : await CreateCompany();

            if (!isTokenCreated) return null;

            await Task.Delay(20000);

            return await Login(tokenType.ToString().ToLower(), "string");
        }

        private async Task<string?> Login(string email, string password)
        {
            using var client = _factory.CreateClient("Default");

            var authResponse = await client.PostAsync(
                "api/auth/login",
                JsonContent.Create(
                new
                {
                    email,
                    password
                }));

            if (authResponse.IsSuccessStatusCode)
            {
                return await authResponse.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }

        private async Task<bool> CreateClient()
        {
            using var client = _factory.CreateClient("Default");

            var clientCreatedResponse = await client.PostAsync(
                "api/clients",
                JsonContent.Create(
                new
                {
                    Name = "string",
                    Email = "client",
                    Password = "string",
                    PhoneNumber = "client",
                    City = "string",
                    Street = "string"
                }));

            return clientCreatedResponse.IsSuccessStatusCode;
        }

        private async Task<bool> CreateCompany()
        {
            using var client = _factory.CreateClient("Default");

            var companyCreatedResponse = await client.PostAsync(
                "api/companies",
                JsonContent.Create(
                new
                {
                    Name = "string",
                    Email = "company",
                    Password = "string",
                    PhoneNumber = "company",
                    City = "string",
                    Street = "string"
                }));

            return companyCreatedResponse.IsSuccessStatusCode;
        }

        public void ClearTokens()
        {
            _clientToken = null;
            _companyToken = null;
        }
    }

    public enum TokenType
    {
        Client,
        Company
    }
}