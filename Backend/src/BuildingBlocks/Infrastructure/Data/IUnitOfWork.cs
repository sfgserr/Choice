using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        bool HasActiveTransaction { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task SaveChangesAsync(IDbContextTransaction transaction);
    }
}
