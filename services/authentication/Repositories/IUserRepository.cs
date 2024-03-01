using Choice.Authentication.Models;

namespace Choice.Authentication.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);

        Task<User> GetByEmail(string email);

        Task<User> GetByPhoneNumber(string phoneNumber);

        Task<bool> Update(User user);
    }
}
