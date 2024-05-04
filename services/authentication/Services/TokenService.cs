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
                Claims = new Dictionary<string, object>() 
                { 
                    ["id"] = user.Id,
                    ["address"] = $"{user.Street},{user.City}",
                    [ClaimTypes.Email] = user.Email,
                    [ClaimTypes.Name] = user.UserName,
                    ["phone"] = user.PhoneNumber,
                    ["type"] = user.UserType.ToString(),
                    ["isDataFilled"] = user.IsDataFilled,
                },
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
