using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Concurrent;

namespace BuildingBlocks.Infrastructure.Data.ValueConversion
{
    public class StronglyTypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies) :
        ValueConverterSelector(dependencies)
    {
        private readonly ConcurrentDictionary<(Type modelClrType, Type providerClrType), ValueConverterInfo> Cache =
            new();

        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type? providerClrType = null)
        {
            var baseSelectors = base.Select(modelClrType, providerClrType);

            foreach (var baseSelector in baseSelectors)
            {
                yield return baseSelector;
            }

            var underlyingModelClrType = UnwrapNullableType(modelClrType);
            var underlyingProviderClrType = UnwrapNullableType(providerClrType);

            if (underlyingProviderClrType is null || underlyingProviderClrType == typeof(Guid))
            {
                bool isTypeIdValue = typeof(TypedIdValueBase).IsAssignableFrom(underlyingModelClrType);

                if (isTypeIdValue)
                {
                    var converterType = typeof(TypedIdValueConverter<>).MakeGenericType(underlyingModelClrType);

                    yield return Cache.GetOrAdd((underlyingModelClrType, typeof(Guid)), _ =>
                    {
                        return new ValueConverterInfo(
                            modelClrType: underlyingModelClrType,
                            providerClrType: typeof(Guid),
                            factory: valueConverterInfo => (ValueConverter)Activator.CreateInstance(converterType, valueConverterInfo.MappingHints));
                    });
                }
            }
        }

        private static Type UnwrapNullableType(Type type)
        {
            if (type is null)
            {
                return null;
            }

            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }
}
