using Choice.Chat.Api.Entities;

namespace Choice.Chat.Api.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> Get(string id);

        Task<bool> Delete(string id);
    }
}
