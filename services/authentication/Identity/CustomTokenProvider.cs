using Choice.Authentication.Api.Models;
using Choice.Authentication.Api.Services;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Authentication.Api.Identity
{
    public class CustomTokenProvider : IUserTwoFactorTokenProvider<User>
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public CustomTokenProvider(ITokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
        {
            return Task.FromResult(true);
        }

        public Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user)
        {
            string token = _tokenService.GenerateToken(
                    user,
                    _configuration["JwtSettings:Key"]!,
                    _configuration["JwtSettings:Issuer"]!,
                    _configuration["JwtSettings:Audience"]!);

            return Task.FromResult(token);
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<User> manager, User user)
        {
            if (purpose != null && purpose == "ResetPassword")
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var id = jwtToken.Claims.FirstOrDefault(c => c.ValueType == "id");

                return Task.FromResult(id is not null && id.Value == user.Id);
            }

            return Task.FromResult(false);
        }
    }
}
