using BuildingBlocks.Domain;

namespace Users.Domain.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmail(string email);
    }
}
