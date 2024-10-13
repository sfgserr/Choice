namespace IntegrationTests.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;

        private string? _clientToken;
        private string? _companyToken;

        public AuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string?> GetClientToken()
        {
            if (_clientToken is null)
            {
                _clientToken = await LoginAsClient();
                return _clientToken;
            }

            return _clientToken;
        }

        public async Task<string?> GetCompanyToken()
        {
            if (_companyToken is null)
            {
                _companyToken = await LoginAsCompany();
                return _companyToken;
            }

            return _companyToken;
        }

        private async Task<string?> LoginAsClient()
        {
            var clientCreated = await CreateClient();

            if (!clientCreated) return null;

            await Task.Delay(20000);

            return await Login("client", "string");
        }

        private async Task<string?> LoginAsCompany()
        {
            var companyCreated = await CreateCompany();

            if (!companyCreated) return null;

            await Task.Delay(20000);

            return await Login("company", "string");       
        }

        private async Task<string?> Login(string email, string password)
        {
            var authResponse = await _client.PostAsync(
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
            var clientCreatedResponse = await _client.PostAsync(
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
            var companyCreatedResponse = await _client.PostAsync(
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
}