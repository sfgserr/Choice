using Choice.Ordering.Application.Services;
using Microsoft.AspNetCore.Http;

namespace Choice.Ordering.Infrastructure.Authentication
{
    public sealed class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId()
        {
            var user = _contextAccessor.HttpContext.User;

            string id = user.FindFirst("id")?.Value!;

            return id;
        }
    }
}
