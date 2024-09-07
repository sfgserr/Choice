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

        public Address Address => 
            new(
                _userService.GetAttribute("city"), 
                _userService.GetAttribute("street"),
                new(
                    _userService.GetAttribute("latitude"), 
                    _userService.GetAttribute("longitude")));
    }
}
