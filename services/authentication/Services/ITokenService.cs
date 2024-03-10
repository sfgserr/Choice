using Choice.Authentication.Models;

namespace Choice.Authentication.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user, string key, string issuer, string audience);
    }
}
