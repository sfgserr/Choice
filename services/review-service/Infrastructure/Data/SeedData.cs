using Choice.ReviewService.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReviewService.Api.Infrastructure.Data
{
    public static class SeedData
    {
        public static readonly string DefaultClientGuid = "59f5697f-84d3-4b7a-97d7-96f9fd2c8c86";

        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Author>()
                   .HasData(new
                   {
                       Guid = DefaultClientGuid,
                       Name = "Макар Чебан",
                       IconUri = "defaulturi"
                   });
        }
    }
}
