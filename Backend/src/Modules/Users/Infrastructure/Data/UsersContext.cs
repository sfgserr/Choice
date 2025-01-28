using BuildingBlocks.Infrastructure.InternalCommands;
using BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Users.Application.Contracts;
using Users.Domain.OrderRequests;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.Users;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;
using Users.Infrastructure.Data.Domain.Clients;
using Users.Infrastructure.Data.Domain.Companies;
using Users.Infrastructure.Data.Domain.OrderRequests;
using Users.Infrastructure.Data.Domain.OrderResponses;
using Users.Infrastructure.Data.Domain.Users;
using Users.Infrastructure.Data.InternalCommands;
using Users.Infrastructure.Data.Outbox;

namespace Users.Infrastructure.Data
{
    public class UsersContext : DbContext, IUsersDbContext
    {
        public UsersContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<OrderRequest> OrderRequests { get; set; }

        public DbSet<OrderResponse> OrderResponses { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderResponseEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
        }
    }
}
