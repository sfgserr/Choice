using Choice.Application.Services;
using Choice.Infrastructure.Data;

namespace Infrastructure.Authentication
{
    public sealed class TestUserService : IUserService
    {
        public string GetUserId() =>
            SeedData.DefaultClientGuid;
    }
}
