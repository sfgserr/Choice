using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts;

namespace WebApi.Modules.Users
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUsersModule _usersModule;

        public UserController(IUsersModule usersModule)
        {
            _usersModule = usersModule;
        }


    }
}
