using System.Data;
using BuildingBlocks.Infrastructure.DomainEventDispatching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly DomainEventsDispatcher _dispatcher;
        
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(DbContext dbContext, DomainEventsDispatcher dispatcher)
        {
            _dbContext = dbContext;
            _dispatcher = dispatcher;
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return _currentTransaction;
            
            _currentTransaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            return _currentTransaction;
        }

        public async Task SaveChangesAsync(IDbContextTransaction transaction)
        {
            _dispatcher.DispatchDomainEvents();
            
            await CommitAsync(transaction);   
        }

        private async Task CommitAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentException("Transaction is null");
            if (transaction != _currentTransaction) throw new InvalidOperationException("Transaction is not current");

            try
            {
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}