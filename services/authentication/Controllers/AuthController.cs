using Authentication.Api.Identity;
using Authentication.Api.Services;
using Choice.Authentication.Api.Models;
using Choice.Authentication.Api.Services;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
        private readonly IPhoneVerificationService _verificationService;
        private readonly IEmailVerificationService _emailVerificationService;

        public AuthController(ITokenService tokenService, UserManager<User> userManager, IConfiguration configuration,
            IPublishEndpoint endPoint, IPhoneVerificationService verificationService, IEmailVerificationService emailVerificationService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _configuration = configuration;
            _endPoint = endPoint;
            _verificationService = verificationService;
            _emailVerificationService = emailVerificationService;
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            User? user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                await _emailVerificationService.SendCode(email);

                return Ok();
            }

            return NotFound();
        }

        [HttpPost("VerifyPasswordReset")]
        public async Task<IActionResult> VerifyPasswordReset(string email, string code)
        {
            User? user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                bool isVerified = _emailVerificationService.VerifyCode(email, code);

                if (isVerified)
                {
                    string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                    return Ok(resetPasswordToken);
                }

                return Unauthorized();
            }

            return NotFound();
        }

        [Authorize]
        [HttpPut("SetNewPassword")]
        public async Task<IActionResult> SetNewPassword(string password)
        {
            string id = HttpContext.User.FindFirst("id")?.Value!;
            string token = HttpContext.Request.Headers.Authorization!.ToString().Replace("Bearer", "").Trim();

            User? user = await _userManager.FindByIdAsync(id);

            if (user is not null)
            {
                var result = await _userManager.ResetPasswordAsync(user, token, password);
                
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    ValidationProblemDetails problemDetails = new(new Dictionary<string, string[]>
                    {
                        ["Token"] = ["Token is invalid"]
                    });

                    return BadRequest(problemDetails);
                }
            }

            return NotFound();
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

            await _verificationService.SendCode(phone);

            return Ok();
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> VerifyCode(string phone, string code)
        {
            bool isVerified = _verificationService.VerifyCode(phone, code);

            if (isVerified)
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
            string street, string city, string phoneNumber, string deviceToken, UserType type)
        {
            if (type == UserType.Admin)
                return BadRequest("You can not register admin account");

            Dictionary<string, string[]> errorMessages = [];

            Regex regex = new(@"\d{10}");

            if (!regex.IsMatch(phoneNumber))
            {
                errorMessages.Add(nameof(phoneNumber), ["Invalid phone"]);
            }

            User? existUser = await _userManager.FindByEmailAsync(email);

            if (existUser != null)
            {
                errorMessages.Add(nameof(email), ["Email already in use"]);
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
                     user.UserName,
                     user.Email,
                     user.City,
                     user.Street,
                     user.PhoneNumber,
                     user.UserType.ToString(),
                     deviceToken));

                return Ok(user);
            }

            return BadRequest(result);
        }
    }
}
