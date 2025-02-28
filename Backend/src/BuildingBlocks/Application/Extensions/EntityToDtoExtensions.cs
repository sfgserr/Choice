using System.Reflection;
using BuildingBlocks.Domain;

namespace BuildingBlocks.Application.Extensions
{
    public static class EntityToDtoExtensions
    {
        public static TDto ToDto<TEntity, TDto>(this TEntity entity) where TEntity : Entity where TDto : new()
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var destination = new TDto();
            var sourceType = typeof(TEntity);
            var destinationType = typeof(TDto);

            var sourceFields = sourceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var sourceProperties = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var destinationProperties = destinationType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var destProp in destinationProperties)
            {
                var sourceProp = sourceProperties.FirstOrDefault(p => p.Name.Equals(destProp.Name, StringComparison.OrdinalIgnoreCase));
                if (sourceProp != null)
                {
                    destProp.SetValue(destination,
                        sourceProp.PropertyType.IsAssignableTo(typeof(ValueObject))
                            ? sourceProp.PropertyType.GetProperty("Value")!.GetValue(sourceProp.GetValue(entity))
                            : sourceProp.GetValue(entity));
                    continue;
                }
                
                var sourceField = sourceFields.FirstOrDefault(f => f.Name.Equals("_" + destProp.Name, StringComparison.OrdinalIgnoreCase));
                if (sourceField != null)
                {
                    destProp.SetValue(destination,
                        sourceField.FieldType.IsAssignableTo(typeof(ValueObject))
                            ? sourceField.FieldType.GetProperty("Value")!.GetValue(sourceField.GetValue(entity))
                            : sourceField.GetValue(entity));
                }
            }

            return destination;
        }
    }
}