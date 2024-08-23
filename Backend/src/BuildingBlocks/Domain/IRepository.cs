
namespace BuildingBlocks.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task Add(T aggregateRoot);

        Task<IList<T>> GetAll();
    }
}
