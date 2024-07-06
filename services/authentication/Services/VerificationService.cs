using System.Text;

namespace Authentication.Api.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly Dictionary<string, string> _verificationResources = [];

        private readonly VerificationOptions _options;
        private readonly HttpClient _httpClient;

        public VerificationService(VerificationOptions options, IHttpClientFactory httpClientFactory)
        {
            _options = options;
            _httpClient = httpClientFactory.CreateClient("Sms");
        }

        public async Task<bool> SendCode(string phone)
        {
            string code = GenerateCode();

            HttpRequestMessage request = new(
                HttpMethod.Post, 
                $"api_key=${_options.ApiKey}&api_secret=${_options.ApiSecret}&to=${phone}&text=${code}&type=text&from=Vybor");

            var result = await _httpClient.SendAsync(request);

            if (result.IsSuccessStatusCode)
            {
                bool isAdd = _verificationResources.TryAdd(phone, code);

                return isAdd;
            }

            return false;
        }

        public bool VerifyCode(string phone, string code)
        {
            if (_verificationResources.ContainsKey(phone))
            {
                return false;
            }

            if (_verificationResources[phone] == code)
            {
                _verificationResources.Remove(phone);
                return true;
            }

            return false;
        }

        private string GenerateCode()
        {
            var random = new Random();

            var stringBuilder = new StringBuilder();

            do
            {
                stringBuilder.Clear();

                for (int i = 0; i < 6; i++)
                    stringBuilder.Append(random.Next(1, 10));
            }
            while (_verificationResources.ContainsValue(stringBuilder.ToString()));

            return stringBuilder.ToString();
        }
    }
}
