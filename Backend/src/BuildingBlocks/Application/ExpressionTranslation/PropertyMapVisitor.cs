using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Application.ExpressionTranslation
{
    internal class PropertyMapVisitor<TData> : ExpressionVisitor
    {
        private readonly Dictionary<string, string> _propertyMappings;
    
        internal PropertyMapVisitor()
        {
            _propertyMappings = DataModelMapper.GetMappings(typeof(TData));
        }
    
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TData) && 
                node.Member is PropertyInfo)
            {
                _propertyMappings.TryGetValue(node.Member.Name, out var entityPropName);
                
                entityPropName ??= node.Member.Name;
                
                return Expression.Call(
                    typeof(EF).GetMethod("Property").MakeGenericMethod(node.Type),
                    node.Expression,
                    Expression.Constant(entityPropName));
            }
        
            return base.VisitMember(node);
        }
    }

    internal static class DataModelMapper
    {
        public static Dictionary<string, string> GetMappings(Type dataModelType)
        {
            var dictionary = new Dictionary<string, string>();
            
            var properties = dataModelType.GetProperties();

            foreach (var property in properties)
            {
                var fieldName = string.Create(property.Name.Length+1, property.Name, (span, str) =>
                {
                    span[0] = '_';
                    str.AsSpan(0).CopyTo(span[1..]);
                    span[1] = char.ToLower(str[0]);
                });
                
                dictionary.Add(property.Name, fieldName);
            }
            
            return dictionary;
        }
    }
}