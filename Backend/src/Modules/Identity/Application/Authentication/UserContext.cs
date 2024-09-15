using BuildingBlocks.Application.Authentication;
using Identity.Domain.Users;

namespace Identity.Application.Authentication
{
    public class UserContext : IUserContext
    {
        private readonly IUserService _userService;

        public UserContext(IUserService userService)
        {
            _userService = userService;
        }

        public UserId Id => new(_userService.GetUserId());
    }
}