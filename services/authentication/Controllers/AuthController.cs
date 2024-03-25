using Choice.Authentication.Api.Models;
using Choice.Authentication.Api.Repositories;
using Choice.Authentication.Api.Services;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace Choice.Authentication.Api.Controllers
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

        [HttpPost("LoginByPhone")]
        public async Task<IActionResult> LoginByPhone(string phone)
        {
            User user = await _repository.GetByPhoneNumber(phone);

            if (user == null)
                return NotFound();

            string serviceId = _configuration["TwilioSettings:ServiceId"];

            var verification = VerificationResource.Create(
                to: $"+{phone}",
                channel: "sms",
                pathServiceSid: serviceId);

            return verification.Status == "pending" ? Ok() : BadRequest();
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> VerifyCode(string phone, string code)
        {
            string serviceId = _configuration["TwilioSettings:ServiceId"];

            var verificationCheck = VerificationCheckResource.Create(
                to: $"+{phone}",
                code: code,
                pathServiceSid: serviceId);

            if (verificationCheck.Status == "approved")
            {
                User user = await _repository.GetByPhoneNumber(phone);

                string token = _tokenService.GenerateToken
                    (user,
                     _configuration["JwtSettings:Key"]!,
                     _configuration["JwtSettings:Issuer"]!,
                     _configuration["JwtSettings:Audience"]!);

                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string email, string name, string password,
            string street, string city, string phoneNumber, UserType type)
        {
            if (type == UserType.Admin)
                return BadRequest();

            Dictionary<string, string[]> errorMessages = new();

            User existUser = await _repository.GetByEmail(email);

            if (existUser != null)
            {
                errorMessages.Add(nameof(email), new[] { "Email already in use" });
            }

            existUser = await _repository.GetByPhoneNumber(phoneNumber);

            if (existUser != null)
            {
                errorMessages.Add(nameof(phoneNumber), new[] { "Phone already in use" });
            }

            if (errorMessages.Count > 0)
            {
                ValidationProblemDetails problemDetails = new(errorMessages);
                return BadRequest(problemDetails);
            }

            User user = new(Guid.NewGuid(), email, password, name, phoneNumber, city, street, type);

            await _repository.Add(user);

            await _endPoint.Publish<UserCreatedEvent>(new
                (user.Id.ToString(),
                 user.Name,
                 user.Email,
                 user.City,
                 user.Street,
                 user.PhoneNumber,
                 user.IconUri,
                 user.UserType.ToString()));

            return Ok(user);
        }
    }
}
