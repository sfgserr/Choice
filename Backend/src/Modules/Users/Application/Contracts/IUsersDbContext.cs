using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using Users.Domain.OrderRequests;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.Users;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Application.Contracts
{
    public interface IUsersDbContext
    {
        DbSet<User> Users { get; }

        DbSet<Company> Companies { get; }

        DbSet<Client> Clients { get; }

        DbSet<OrderRequest> OrderRequests { get; }
        
        DbSet<OrderResponse> OrderResponses { get; }
    }
}