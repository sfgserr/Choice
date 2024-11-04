using BuildingBlocks.Application.Authentication;
using Chat.Domain.ChatUsers;

namespace Chat.Application.ChatUsers
{
    public class UserContext : IUserContext
    {
        private readonly IUserService _userService;

        public UserContext(IUserService userService)
        {
            _userService = userService;
        }

        public ChatUserId Id => new(_userService.GetUserId());
    }
}