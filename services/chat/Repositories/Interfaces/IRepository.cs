
namespace Choice.Chat.Api.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);

        Task<IList<T>> GetAll();

        Task Update(T entity);
    }
}
