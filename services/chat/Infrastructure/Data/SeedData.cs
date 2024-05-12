using Choice.Chat.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Choice.Chat.Api.Infrastructure.Data
{
    public static class SeedData
    {
        private static string UserGuid = "59f5697f-84d3-4b7a-97d7-96f9fd2c8c86";

        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<User>()
                   .HasData(new
                   {
                       Guid = UserGuid,
                       IconUri = "defaulturi",
                       Name = "Makar_Cheban"
                   });
        }
    }
}
