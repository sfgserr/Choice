using Choice.Authentication.Models;
using Choice.Authentication.Repositories;
using Choice.Authentication.Services;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _repository;
        private readonly IPublishEndpoint _endPoint;
        private readonly IConfiguration _configuration;

        public AuthController(ITokenService tokenService, IConfiguration configuration, IUserRepository repository, IPublishEndpoint endPoint)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _repository = repository;
            _endPoint = endPoint;
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

        [HttpPost("RegisterAsClient")]
        public async Task<IActionResult> RegisterClient(string email, string name, string surname, string password,
            string street, string city)
        {
            User user = new(Guid.NewGuid(), email, password, $"{surname} {name}", string.Empty, city, street);

            await _repository.Add(user);

            await _endPoint.Publish<UserCreatedEvent>(new
                (user.Id.ToString(),
                 user.Name,
                 user.Email,
                 user.City,
                 user.Street,
                 user.PhoneNumber));
        }
    }
}
