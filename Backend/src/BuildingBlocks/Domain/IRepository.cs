
namespace BuildingBlocks.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task Add(T company);

        Task<IList<T>> GetAll();
    }
}
