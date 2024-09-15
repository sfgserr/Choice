using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts;

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
    }
}
