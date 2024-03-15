using Choice.Authentication.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Choice.Authentication.Api.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(User user, string key, string issuer, string audience)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] tokenKey = Encoding.UTF8.GetBytes(key);

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddDays(2),
                Issuer = issuer,
                Audience = audience,
                Claims = new Dictionary<string, object>() { ["id"] = user.Id },
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            KeyValuePair<string, object> claimToAdd = user.UserType switch
            {
                UserType.Company => new KeyValuePair<string, object>("address", $"{user.Street},{user.City}"),
                UserType.Client => new KeyValuePair<string, object>(ClaimTypes.Email, user.Email),
                UserType.Admin => new KeyValuePair<string, object>("Admin", true),
                _ => throw new ArgumentException(nameof(user.UserType))
            };

            securityTokenDescriptor.Claims.Add(claimToAdd);

            var token = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
