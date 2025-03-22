using Administration.Domain.Categories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Administration.Infrastructure.Data.Domain.Categories
{
    internal class CategoryIdValueGenerator : ValueGenerator
    {
        protected override object? NextValue(EntityEntry entry)
        {
            return new CategoryId(0);
        }

        public override bool GeneratesTemporaryValues => true;
    }
}