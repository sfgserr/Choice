using Choice.Authentication.Models;
using Choice.Authentication.Repositories;

namespace Choice.Authentication
{
    public static class UsersSeed
    {
        public static async Task Seed(IServiceProvider services)
        {
            var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

            IUserRepository repository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            User user = new
                (Guid.NewGuid(),
                 "email@gmail.com",
                 "Alex",
                 "PaSsWoRd",
                 "37377875397",
                 "Москва",
                 "Арбат 26",
                 UserType.Client);

            await repository.Add(user);
        }
    }
}
