using Choice.Authentication.Models;
using Choice.Authentication.Repositories;
using Choice.Authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthController(ITokenService tokenService, IConfiguration configuration, IUserRepository repository)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _repository = repository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await _repository.GetByEmail(email);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Password != password)
            {
                return Unauthorized();
            }

            string token = _tokenService.GenerateToken
                (user.Id.ToString(),
                _configuration["JwtSettings:Key"]!,
                _configuration["JwtSettings:Issuer"]!,
                _configuration["JwtSettings:Audience"]!);

            return Ok(token);
        }
    }
}
