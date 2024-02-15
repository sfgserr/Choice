using Choice.Authentication.Entities;
using Choice.Authentication.Infrastructure.Data.Repositories;
using Choice.Authentication.Infrastructure.Verification.Interfaces;
using Choice.Authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISmsService _smsService;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public AuthController(IUserRepository userRepository, IConfiguration configuration, TokenService tokenService, ISmsService smsService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _tokenService = tokenService;
            _smsService = smsService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await _userRepository.GetUserByEmail(email);

            if (user == null)
                return Unauthorized();

            if (user.Password != password)
                return Unauthorized();

            string token = _tokenService.GenerateToken(user.Id, _configuration["Auth:Key"], _configuration["Auth:Issuer"],
                _configuration["Auth:Audience"]);

            return Ok(token); 
        }

        [HttpPost("LoginByPhone")]
        public async Task<IActionResult> LoginByPhone(string phone)
        {
            User user = await _userRepository.GetUserByPhone(phone);

            if (user == null) 
                return Unauthorized();

            bool result = await _smsService.SendVerificationCode(phone);

            return result ? Ok() : BadRequest();
        }

        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode(string phone, string code, int userId)
        {
            bool result = await _smsService.CheckVerificationCode(phone, code);

            if (!result)
                return BadRequest();

            string token = _tokenService.GenerateToken(userId, _configuration["Auth:Key"], _configuration["Auth:Issuer"],
                _configuration["Auth:Audience"]);

            return Ok(token);
        }
    }
}
