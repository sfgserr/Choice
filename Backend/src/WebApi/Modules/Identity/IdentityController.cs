using System.Security.Claims;
using BuildingBlocks.Infrastructure.Authorization;
using Identity.Application.Authentication.Authenticate;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Modules.Identity
{
    [Route("api/auth")]
    public class IdentityController : Controller
    {
        private readonly IIdentityModule _identityModule;
        private readonly JwtProvider _jwtProvider;
        
        public IdentityController(IIdentityModule identityModule, JwtProvider jwtProvider)
        {
            _identityModule = identityModule;
            _jwtProvider = jwtProvider;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _identityModule.ExecuteCommand<AuthenticateCommand, AuthenticationResult>(
                new AuthenticateCommand(
                    request.Email,
                    request.Password));

            return result.IsSuccessfull
                ? Ok(_jwtProvider.GenerateToken([new Claim("id", result.UserId.ToString()!)]))
                : Unauthorized(result.ErrorMessage);
        }
    }
}