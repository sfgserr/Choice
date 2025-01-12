
namespace Identity.Infrastructure.Authorization
{
    public class IdentityOptions
    {
        public IdentityOptions(string issuer, string secretKey, string pathToCert)
        {
            Issuer = issuer;
            SecretKey = secretKey;
            PathToCert = pathToCert;
        }

        public string Issuer { get; }

        public string SecretKey { get; }
        
        public string PathToCert { get; }
    }
}
