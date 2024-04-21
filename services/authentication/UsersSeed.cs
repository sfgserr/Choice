﻿using Choice.Authentication.Api.Models;
using Choice.Authentication.Api.Repositories;

namespace Choice.Authentication
{
    public static class UsersSeed
    {
        public static readonly Guid ClientGuid = new ("59f5697f-84d3-4b7a-97d7-96f9fd2c8c86");

        public static async Task Seed(IServiceProvider services)
        {
            var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

            IUserRepository repository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            User admin = new
                (Guid.NewGuid(),
                 "email@gmail.com",
                 "PaSsWoRd",
                 "Alex",
                 "37377753296",
                 "Москва",
                 "Арбат 26",
                 UserType.Admin);

            User client = new
                (ClientGuid,
                 "dead01r@gmail.com",
                 "PaSsWoRd",
                 "Makar Cheban",
                 "37377875397",
                 "Москва",
                 "Арбат 26",
                 UserType.Client);

            await repository.Add(admin);
            await repository.Add(client);
        }
    }
}
