using System.Linq.Expressions;
using BuildingBlocks.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Application.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task<TEntity> Get<TEntity>(
            this DbSet<TEntity> dbSet, 
            Expression<Func<TEntity, bool>> expression) 
            where TEntity : class
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);

            return ThrowIfNull(entity);
        }
        
        public static async Task<TEntity> GetAsNoTracking<TEntity>(
            this DbSet<TEntity> dbSet, 
            Expression<Func<TEntity, bool>> expression) 
            where TEntity : class
        {
            var entity = await dbSet.AsNoTracking().FirstOrDefaultAsync(expression);

            return ThrowIfNull(entity);
        }

        private static TEntity ThrowIfNull<TEntity>(TEntity? entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new InvalidCommandException([$"Entity of type {typeof(TEntity).Name} is not found"]);
            }

            return entity;
        }
    }
}