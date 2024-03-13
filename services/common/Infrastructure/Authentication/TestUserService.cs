using Choice.Application.Services;
using Choice.Infrastructure.Data;

namespace Choice.Infrastructure.Authentication
{
    public sealed class TestUserService : IUserService
    {
        public string GetUserId() =>
            SeedData.DefaultClientGuid;
    }
}
