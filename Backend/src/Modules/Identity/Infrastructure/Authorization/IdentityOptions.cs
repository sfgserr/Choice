
namespace Identity.Infrastructure.Authorization
{
    public class IdentityOptions
    {
        public IdentityOptions(string issuer, string secretKey, string pathToCert, string certificatePassword)
        {
            Issuer = issuer;
            SecretKey = secretKey;
            PathToCert = pathToCert;
            CertificatePassword = certificatePassword;
        }

        public string Issuer { get; }

        public string SecretKey { get; }
        
        public string PathToCert { get; }
        
        public string CertificatePassword { get; }
    }
}
