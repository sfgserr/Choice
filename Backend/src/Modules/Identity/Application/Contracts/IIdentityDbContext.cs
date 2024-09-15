using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Contracts
{
    public interface IIdentityDbContext
    {
        DbSet<User> Users { get; }
    }
}