using Choice.Domain.Models;

namespace Choice.Domain
{
    public interface IRepository<TEntity> where TEntity : DomainObject
    {
        Task<TEntity> Create(TEntity entity);

        Task<IList<TEntity>> Get();

        Task<TEntity> GetBy(Func<TEntity, bool> func);

        Task<TEntity> Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}
