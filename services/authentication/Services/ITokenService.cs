namespace Choice.Authentication.Services
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string key, string issuer, string audience);
    }
}
