using Choice.Authentication.Entities;
using Choice.Authentication.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

            List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.Id.ToString()) };
            var jwt = new JwtSecurityToken(
                issuer: "http://host.docker.internal:8081",
                audience: "http://host.docker.internal:8080",
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)));

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(token); 
        }
    }
}
