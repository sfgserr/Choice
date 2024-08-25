
namespace BuildingBlocks.Infrastructure.Authorization
{
    public class JwtOptions
    {
        public JwtOptions(string issuer, string audience, string secretKey)
        {
            Issuer = issuer;
            Audience = audience;
            SecretKey = secretKey;
        }

        public string Issuer { get; }

        public string Audience { get; }

        public string SecretKey { get; }
    }
}
