using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Users.Application.Contracts;
using Users.Application.Users.Commands.Auth;

namespace WebApi.Modules.Users
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : Controller
    {
        private readonly IUsersModule _usersModule;
        private readonly JwtProvider _jwtProvider;

        public UserController(IUsersModule usersModule, JwtProvider jwtProvider)
        {
            _usersModule = usersModule;
            _jwtProvider = jwtProvider;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            string password = Encoding.UTF8.GetString(Convert.FromBase64String(request.EncodedPassword));

            var result = await _usersModule.ExecuteCommand<AuthCommand, AuthResult>(new AuthCommand(
                request.Email,
                password));

            if (!result.IsSuccessfull)
                return BadRequest(new { result.ErrorMessage });

            var userId = result.UserId;

            return Ok(new
            {
                AccessToken = _jwtProvider.GenerateToken([new("id", userId.ToString()!)]),
                RefreshToken = _jwtProvider.GetOrAddRefreshToken(userId!.Value)
            });
        }
    }
}
