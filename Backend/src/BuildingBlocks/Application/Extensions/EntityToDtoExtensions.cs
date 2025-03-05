using System.Reflection;
using BuildingBlocks.Domain;

namespace BuildingBlocks.Application.Extensions
{
    public static class EntityToDtoExtensions
    {
        public static TDto ToDto<TDto>(this Entity entity) where TDto : new()
        {
            return ToDto<TDto>(entity, new());
        }
        
        private static TDto ToDto<TDto>(Entity entity, TDto destination) where TDto : new()
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            var sourceType = entity.GetType();
            var destinationType = typeof(TDto);

            var sourceFields = sourceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var sourceProperties = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var destinationProperties = destinationType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var sourceProp in sourceProperties)
            {
                if (sourceProp.PropertyType.IsAssignableTo(typeof(Entity)) && sourceProp.GetValue(entity) is Entity entityProp)
                {
                    ToDto(entityProp, destination);
                    continue;
                }
                
                var destProp = destinationProperties.FirstOrDefault(p => p.Name.Equals(sourceProp.Name, StringComparison.OrdinalIgnoreCase));
                if (destProp != null)
                {
                    destProp.SetValue(destination,
                        sourceProp.PropertyType.IsAssignableTo(typeof(ValueObject))
                            ? sourceProp.PropertyType.GetProperty("Value")!.GetValue(sourceProp.GetValue(entity))
                            : sourceProp.GetValue(entity));
                }
            }
            
            foreach (var sourceField in sourceFields)
            {
                if (sourceField.FieldType.IsAssignableTo(typeof(Entity)) && sourceField.GetValue(entity) is Entity entityField)
                {
                    ToDto(entityField, destination);
                    continue;
                }
                
                var destProp = destinationProperties.FirstOrDefault(p => 
                    p.Name.Equals(char.ToUpper(sourceField.Name[1]) + sourceField.Name[2..], StringComparison.OrdinalIgnoreCase));
                if (destProp != null)
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