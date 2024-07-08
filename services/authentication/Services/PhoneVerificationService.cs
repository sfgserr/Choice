
namespace Authentication.Api.Services
{
    public class PhoneVerificationService : IPhoneVerificationService
    {
        private readonly VerificationOptions _options;
        private readonly HttpClient _httpClient;

        public PhoneVerificationService(VerificationOptions options, IHttpClientFactory httpClientFactory)
        {
            _options = options;
            _httpClient = httpClientFactory.CreateClient("Sms");
        }

        public async Task<bool> SendCode(string phone)
        {
            string code = CodeManager.GenerateCode(phone);

            HttpRequestMessage request = new(
                HttpMethod.Post, 
                $"api_key=${_options.ApiKey}&api_secret=${_options.ApiSecret}&to=${phone}&text=${code}&type=text&from=Vybor");

            var result = await _httpClient.SendAsync(request);

            return result.IsSuccessStatusCode;
        }

        public bool VerifyCode(string phone, string code)
        {
            return CodeManager.Check(phone, code);
        }
    }
}
