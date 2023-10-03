using Choice.Domain.Models;

namespace Choice.Domain
{
    public interface IRepository<TEntity> where TEntity : DomainObject
    {
        Task<TEntity> CreateUser(TEntity user);

        Task<IList<TEntity>> GetUsers();

        Task<TEntity> GetUserBy(Func<TEntity, bool> func);

        Task<TEntity> Update(TEntity user);

        Task<TEntity> Delete(TEntity user);
    }
}
