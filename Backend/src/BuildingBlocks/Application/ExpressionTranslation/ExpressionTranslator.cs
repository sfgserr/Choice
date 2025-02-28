using System.Linq.Expressions;

namespace BuildingBlocks.Application.ExpressionTranslation
{
    public static class ExpressionTranslator
    {
        public static Expression<Func<TEntity, T>> Translate<TEntity, TData, T>(
            Expression<Func<TData, T>> dataExpression)
        {
            var visitor = new PropertyMapVisitor<TData>();
            var translatedBody = visitor.Visit(dataExpression.Body);
            
            var entityParam = Expression.Parameter(typeof(TEntity), "e");
            
            var paramReplacer = new ParameterReplacerVisitor(dataExpression.Parameters[0], entityParam);
            var finalBody = paramReplacer.Visit(translatedBody);
            
            return Expression.Lambda<Func<TEntity, T>>(finalBody, entityParam);
        }
    }
}