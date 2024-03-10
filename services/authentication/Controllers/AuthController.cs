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
                (user,
                 _configuration["JwtSettings:Key"]!,
                 _configuration["JwtSettings:Issuer"]!,
                 _configuration["JwtSettings:Audience"]!);

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string email, string name, string surname, string password,
            string street, string city, string phoneNumber, UserType type)
        {
            Dictionary<string, string[]> errorMessages = new();

            User existUser = await _repository.GetByEmail(email);

            if (existUser != null)
            {
                errorMessages.Add(nameof(existUser), new[] { "Email already in use" });
            }

            existUser = await _repository.GetByPhoneNumber(phoneNumber);

            if (existUser != null)
            {
                if (errorMessages.TryGetValue(nameof(existUser), out string[] messages))
                    errorMessages[nameof(existUser)][1] = "Phone number already in user";
                else
                    errorMessages.Add(nameof(existUser), new[] { "Phone already in use" });
            }

            if (errorMessages.Count > 0)
            {
                ValidationProblemDetails problemDetails = new(errorMessages);
                return BadRequest(problemDetails);
            }

            User user = new(Guid.NewGuid(), email, password, $"{surname} {name}", phoneNumber, city, street, type);

            await _repository.Add(user);

            await _endPoint.Publish<UserCreatedEvent>(new
                (user.Id.ToString(),
                 name,
                 surname,
                 user.Email,
                 user.City,
                 user.Street,
                 user.PhoneNumber,
                 user.UserType.ToString()));

            return Ok(user);
        }
    }
}
