using Choice.Ordering.Application.Services;

namespace Choice.Ordering.Infrastructure.Authentication
{
    public sealed class UserServiceFake : IUserService
    {
        public string GetUserId() => "1";
    }
}
