using Choice.Authentication.Entities;

namespace Choice.Authentication.Infrastructure.Data.Repositories
{
    public interface IUserRepository
    {
        Task Create(User user);

        Task<User> GetUser(int id);

        Task<User> GetUserByEmail(string email);

        Task<User> GetUserByPhone(string phone);

        void Update(User user);

        Task Delete(int id);
    }
}
