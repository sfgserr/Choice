using Choice.Authentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace Choice.Authentication.Infrastructure.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }   
    }
}
