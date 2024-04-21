using Choice.ClientService.Domain.ClientAggregate;
using Choice.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Choice.ClientService.Infrastructure.Data
{
    public static class SeedData
    {
        public static readonly string DefaultClientGuid = "59f5697f-84d3-4b7a-97d7-96f9fd2c8c86";

        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Client>(c =>
            {
                c.HasData(new
                {
                    Id = 1,
                    ClientId = 1,
                    Guid = DefaultClientGuid,
                    Name = "Makar",
                    Surname = "Cheban",
                    Email = "dead01r@gmail.com",
                    IconUri = "defaulturi",
                    PhoneNumber = "37377875397",
                    AverageGrade = (double)0,
                    ReviewCount = 0
                });

                c.OwnsOne(c => c.Address)
                 .HasData(new
                 {
                     ClientId = 1,
                     City = "Москва",
                     Street = "Арбат 26"
                 });
            });
        }
    }
}
