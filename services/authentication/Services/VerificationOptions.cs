namespace Authentication.Api.Services
{
    public class VerificationOptions
    {
        public VerificationOptions(string apiKey, string apiSecret)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
        }

        public string ApiKey { get; }
        public string ApiSecret { get; }
    }
}
