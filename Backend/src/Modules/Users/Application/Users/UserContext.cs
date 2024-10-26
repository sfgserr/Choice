using BuildingBlocks.Application.Authentication;
using Users.Domain.Users;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

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
        
        public CompanyId CompanyId => new(_userService.GetUserId());

        public ClientId ClientId => new(_userService.GetUserId());
        
        public Address Address => 
            new(
                _userService.GetAttribute("city"), 
                _userService.GetAttribute("street"),
                new(
                    _userService.GetAttribute("latitude"), 
                    _userService.GetAttribute("longitude")));

        public UserRole Role => UserRole.Parse(_userService.GetAttribute("role"));
    }
}
