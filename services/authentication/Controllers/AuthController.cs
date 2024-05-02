using Choice.Authentication.Api.Models;
using Choice.Authentication.Api.Services;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilio.Rest.Verify.V2.Service;

namespace Choice.Authentication.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IPublishEndpoint _endPoint;
        private readonly IConfiguration _configuration;

        public AuthController(ITokenService tokenService, UserManager<User> userManager, IConfiguration configuration, 
            IPublishEndpoint endPoint)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _configuration = configuration;
            _endPoint = endPoint;
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            string id = HttpContext.User.FindFirst("id")?.Value!;

            User? user = await _userManager.FindByIdAsync(id);

            if (user is not null)
            {
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    ValidationProblemDetails problemDetails = new (new Dictionary<string, string[]> 
                    { 
                        ["oldPassword"] = ["Password did not match"]
                    });

                    return BadRequest(problemDetails);
                }
            }

            return NotFound();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            User? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            bool result = await _userManager.CheckPasswordAsync(user, password);

            if (result)
            {
                string token = _tokenService.GenerateToken
                    (user,
                     _configuration["JwtSettings:Key"]!,
                     _configuration["JwtSettings:Issuer"]!,
                     _configuration["JwtSettings:Audience"]!);

                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost("LoginByPhone")]
        public async Task<IActionResult> LoginByPhone(string phone)
        {
            User? user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);

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
                User user = (await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone))!;

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

            User? existUser = await _userManager.FindByEmailAsync(email);

            if (existUser != null)
            {
                errorMessages.Add(nameof(email), new[] { "Email already in use" });
            }

            existUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

            if (existUser != null)
            {
                errorMessages.Add(nameof(phoneNumber), ["Phone already in use"]);
            }

            if (errorMessages.Count > 0)
            {
                ValidationProblemDetails problemDetails = new(errorMessages);
                return BadRequest(problemDetails);
            }

            User user = new(Guid.NewGuid().ToString(), email, name, phoneNumber, city, street, type);

            var result = await _userManager.CreateAsync(user,  password);

            if (result.Succeeded)
            {
                await _endPoint.Publish<UserCreatedEvent>(new
                    (user.Id.ToString(),
                     user.Name,
                     user.Email,
                     user.City,
                     user.Street,
                     user.PhoneNumber,
                     user.UserType.ToString()));

                return Ok(user);
            }

            return BadRequest();
        }
    }
}
