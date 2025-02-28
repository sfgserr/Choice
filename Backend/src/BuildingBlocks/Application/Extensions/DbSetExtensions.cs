using System.Linq.Expressions;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.ExpressionTranslation;
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

        public static IEnumerable<TEntity> TranslatedWhere<TEntity, TData>(
            this DbSet<TEntity> dbSet,
            Expression<Func<TData, bool>> expression) where TEntity : class
        {
            return dbSet.Where(ExpressionTranslator.Translate<TEntity, TData, bool>(expression));
        }
        
        public static IOrderedEnumerable<TEntity> TranslatedOrderByDescending<TEntity, TData, TKey>(
            this DbSet<TEntity> dbSet,
            Expression<Func<TData, TKey>> expression) where TEntity : class
        {
            var translated = ExpressionTranslator.Translate<TEntity, TData, TKey>(expression);
            
            return dbSet.OrderByDescending(translated.Compile());
        }
        
        public static IOrderedEnumerable<TEntity> TranslatedOrderByDescending<TEntity, TData, TKey>(
            this IEnumerable<TEntity> dbSet,
            Expression<Func<TData, TKey>> expression) where TEntity : class
        {
            var translated = ExpressionTranslator.Translate<TEntity, TData, TKey>(expression);
            
            return dbSet.OrderByDescending(translated.Compile());
        }
        
        private static TEntity ThrowIfNull<TEntity>(TEntity? entity) where TEntity : class
        {
            InvalidCommandException.ThrowIfNull(entity);

            return entity!;
        }
    }
}