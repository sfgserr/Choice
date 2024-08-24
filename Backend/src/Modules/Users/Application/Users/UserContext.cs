using BuildingBlocks.Application.Authentication;
using Users.Domain.Users;

namespace Users.Application.Users
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
