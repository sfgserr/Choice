using Choice.ClientService.Application.Services;
using Choice.ClientService.Infrastructure.Data;

namespace Choice.ClientService.Infrastructure.Authentication
{
    public sealed class TestUserService : IUserService
    {
        public string GetUserId() => SeedData.DefaultClientGuid;
    }
}
