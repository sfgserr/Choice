using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data;
using Users.Domain.Categories;

namespace Users.Infrastructure.Data.ValueConversion
{
    internal class CategoryIdCollectionToIntCollectionValueConverter : 
        ValueConverter<List<CategoryId>, IEnumerable<int>>
    {
        public CategoryIdCollectionToIntCollectionValueConverter(ConverterMappingHints? mappingHints = null) : 
            base(x => x.Select(id => id.Value).ToArray(), x => x.Select(id => new CategoryId(id)).ToList(), mappingHints)
        {
        }
    }
}
