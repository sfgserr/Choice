
namespace Choice.Chat.Api.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T message);

        Task<T> Get(int id);

        Task<IList<T>> GetAll(string senderId, string receiverId);

        void Update(T message);

        Task<bool> Delete(int id);
    }
}
