using Choice.Authentication.Entities;
using Choice.Authentication.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Choice.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await _userRepository.GetUserByEmail(email);

            if (user == null)
                return Unauthorized();

            if (user.Password != password)
                return Unauthorized();

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] tokenKey = Encoding.UTF8.GetBytes("DamnThatSecretKeyWithMinimumLengthOfOneTwentyEightBytes");

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddDays(2),
                Issuer = "http://172.21.112.1",
                Audience = "http://localhost",
                Claims = new Dictionary<string,object>() { ["userid"] = user.Id.ToString() },
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(securityTokenDescriptor);

            return Ok(tokenHandler.WriteToken(token)); 
        }
    }
}
