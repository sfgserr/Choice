using Choice.Application.Services;
using Microsoft.AspNetCore.Http;

namespace Choice.Infrastructure.Authentication
{
    public sealed class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;

        public UserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserId() =>
            _context.HttpContext.User.FindFirst("id")?.Value!;

        public string GetUserType() =>
            _context.HttpContext.User.FindFirst("type")?.Value!;
    }
}
