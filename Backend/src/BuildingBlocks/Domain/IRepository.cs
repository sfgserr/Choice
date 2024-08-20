
namespace BuildingBlocks.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void Add(T aggregateRoot);

        Task<IList<T>> GetAll();
    }
}
