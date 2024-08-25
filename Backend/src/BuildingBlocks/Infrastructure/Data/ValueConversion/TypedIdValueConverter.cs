using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlocks.Infrastructure.Data.ValueConversion
{
    public class TypedIdValueConverter<TTypedIdValueBase> : ValueConverter<TTypedIdValueBase, Guid> 
        where TTypedIdValueBase : TypedIdValueBase
    {
        public TypedIdValueConverter(ConverterMappingHints? hints = null) : base(id => id.Value, id => Create(id), hints)
        {

        }

        private static TTypedIdValueBase Create(Guid id) =>
            Activator.CreateInstance(typeof(TTypedIdValueBase), id) as TTypedIdValueBase;
    }
}
