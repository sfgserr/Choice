using Choice.ClientService.Domain.ClientAggregate;
using Choice.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Choice.ClientService.Infrastructure.Data
{
    public static class SeedData
    {
        public static readonly string DefaultClientGuid = "5a453df7-de7c-4292-a531-969ca6eaee45";

        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Client>()
                .OwnsOne(c => c.Address)
                .HasData(new
                {
                    Id = 1,
                    Guid = DefaultClientGuid,
                    Name = "Makar",
                    Surname = "Cheban",
                    Email = "dead01r@gmail.com",
                    Address = new Address("Арбат 26", "Москва"),
                    IconUri = "defaulturi",
                    PhoneNumber = "37377875397",
                    AverageGrade = (double)0,
                    ReviewCount = 0
                });
        }
    }
}
