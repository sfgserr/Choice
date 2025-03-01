using System.Linq.Expressions;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.ExpressionTranslation;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Application.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task<TEntity> Get<TEntity>(
            this IQueryable<TEntity> dbSet, 
            Expression<Func<TEntity, bool>> expression) 
            where TEntity : class
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);

            return ThrowIfNull(entity);
        }

        public static IQueryable<TEntity> TranslatedWhere<TEntity, TData>(
            this IQueryable<TEntity> dbSet,
            Expression<Func<TData, bool>> expression) where TEntity : class
        {
            return dbSet.Where(ExpressionTranslator.Translate<TEntity, TData, bool>(expression));
        }
        
        public static IOrderedQueryable<TEntity> TranslatedOrderByDescending<TEntity, TData, TKey>(
            this IQueryable<TEntity> dbSet,
            Expression<Func<TData, TKey>> expression) where TEntity : class
        {
            return dbSet.OrderByDescending(ExpressionTranslator.Translate<TEntity, TData, TKey>(expression));
        }
        
        private static TEntity ThrowIfNull<TEntity>(TEntity? entity) where TEntity : class
        {
            InvalidCommandException.ThrowIfNull(entity);

            return entity!;
        }
    }
}