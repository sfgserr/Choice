using Choice.Authentication.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Choice.Authentication
{
    public static class UsersSeed
    {
        public static readonly Guid ClientGuid = new ("59f5697f-84d3-4b7a-97d7-96f9fd2c8c86");

        public static async Task Seed(IServiceProvider services)
        {
            var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            User admin = new
                (Guid.NewGuid().ToString(),
                 "email@gmail.com",
                 "Alex",
                 "37377753296",
                 "Москва",
                 "Арбат 26",
                 UserType.Admin);

            User client = new
                (ClientGuid.ToString(),
                 "dead01r@gmail.com",
                 "Makar Cheban",
                 "37377875397",
                 "Москва",
                 "Арбат 26",
                 UserType.Client);

            await userManager.CreateAsync(admin, "PaSsWoRd");
            await userManager.CreateAsync(client, "PaSsWoRd");
        }
    }
}
