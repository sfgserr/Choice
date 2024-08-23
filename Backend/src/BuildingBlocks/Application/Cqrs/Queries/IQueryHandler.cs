
namespace BuildingBlocks.Application.Cqrs.Queries
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }
}
