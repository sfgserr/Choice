
namespace Choice.Chat.Api.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);

        Task<IList<T>> GetAll();

        void Update(T entity);
    }
}
