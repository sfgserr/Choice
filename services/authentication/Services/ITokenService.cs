using Choice.Authentication.Api.Models;

namespace Choice.Authentication.Api.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user, string key, string issuer, string audience);
    }
}
