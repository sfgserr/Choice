using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Authorization
{
    public class JwtProvider
    {
        private readonly Dictionary<Guid, RefreshToken> _refreshTokens = [];

        private readonly JwtOptions _jwtOptions;

        public JwtProvider(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string GenerateToken(List<Claim> claims)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(12),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public Guid GetOrAddRefreshToken(Guid userId)
        {
            bool isExist = _refreshTokens.TryGetValue(userId, out var refreshToken);

            if (isExist && refreshToken.ExpireDate < DateTime.Now)
            {
                return refreshToken.Token;
            }
            else
            {
                _refreshTokens.Add(userId, new(Guid.NewGuid(), DateTime.Now.AddHours(18)));

                return _refreshTokens[userId].Token;
            }
        }
    }

    public struct RefreshToken
    {
        public RefreshToken(Guid token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }

        public Guid Token { get; }

        public DateTime ExpireDate { get; }
    }
}
