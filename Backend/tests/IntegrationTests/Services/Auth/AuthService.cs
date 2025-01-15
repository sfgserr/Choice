using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace IntegrationTests.Services.Auth
{
    public class AuthService
    {
        private const string Password = "12345678";
        
        private readonly IHttpClientFactory _factory;
        private readonly AppOptions _appOptions;
        
        private string? _clientToken;
        private string? _companyToken;
        
        public AuthService(IHttpClientFactory factory, IOptions<AppOptions> appOptions)
        {
            _factory = factory;
            _appOptions = appOptions.Value;
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

            await Task.Delay(10000);

            return await Login($"{tokenType.ToString().ToLower()}@gmail.com", Password);
        }

        private async Task<string?> Login(string email, string password)
        {
            using var client = _factory.CreateClient("Default");
            
            var request = new HttpRequestMessage(HttpMethod.Post, "api/auth/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["username"] = email,
                ["password"] = password,
                ["scope"] = "offline_access",
                ["client_id"] = _appOptions.ClientId,
                ["client_secret"] = _appOptions.ClientSecret,
            });
            
            var authResponse = await client.SendAsync(request);

            if (authResponse.IsSuccessStatusCode)
            {
                var content = await authResponse.Content.ReadAsStringAsync();
                
                var jObject = JObject.Parse(content);
                
                return jObject.SelectToken("access_token")!.Value<string>();
            }

            return null;
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
                    Email = "client@gmail.com",
                    Password = "12345678",
                    PhoneNumber = "9267339971",
                    City = "Москва",
                    Street = "Ангарская 21"
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
                    Email = "company@gmail.com",
                    Password = "12345678",
                    PhoneNumber = "9267339972",
                    City = "Москва",
                    Street = "Арбат 26"
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