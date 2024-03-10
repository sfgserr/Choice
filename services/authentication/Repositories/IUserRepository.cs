using Choice.Authentication.Api.Models;

namespace Choice.Authentication.Api.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);

        Task<User> Get(string guid);

        Task<User> GetByEmail(string email);

        Task<User> GetByPhoneNumber(string phoneNumber);

        Task<bool> Update(User user);
    }
}
