using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure
{
    public class ChoiceContext : DbContext
    {
        public ChoiceContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMessage> OrderMessages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
    }
}
