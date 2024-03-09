using Choice.Application.Services;
using Microsoft.AspNetCore.Http;

namespace Choice.ClientService.Infrastructure.Authentication
{
    public sealed class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;

        public UserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserId()
        {
            return _context.HttpContext.User.FindFirst("id")?.Value!;
        }
    }
}
