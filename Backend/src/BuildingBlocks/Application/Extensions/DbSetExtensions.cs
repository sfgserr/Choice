using BuildingBlocks.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Application.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task<TEntity> Get<TEntity>(this DbSet<TEntity> dbSet, Func<TEntity, bool> predicate) 
            where TEntity : class
        {
            var entity = await dbSet.FirstOrDefaultAsync(e => predicate(e));

            if (entity == null)
            {
                throw new InvalidCommandException([$"Entity of type {typeof(TEntity).Name} is not found"]);
            }

            return entity;
        }
        
        public static async Task<TEntity> GetAsNoTracking<TEntity>(this DbSet<TEntity> dbSet, Func<TEntity, bool> predicate) 
            where TEntity : class
        {
            var entity = await dbSet.AsNoTracking().FirstOrDefaultAsync(e => predicate(e));

            if (entity == null)
            {
                throw new InvalidCommandException([$"Entity of type {typeof(TEntity).Name} is not found"]);
            }

            return entity;
        }
    }
}