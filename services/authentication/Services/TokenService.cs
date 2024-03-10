using Choice.Authentication.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Choice.Authentication.Services
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

            string address = $"{user.Street},{user.City}";

            if (user.UserType == UserType.Company)
                securityTokenDescriptor.Claims.Add(nameof(address), address);

            var token = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
