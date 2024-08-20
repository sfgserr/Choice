using BuildingBlocks.Domain;

namespace Users.Domain.Categories
{
    public class CategoryId : ValueObject
    {
        public int Value { get; }

        public CategoryId(int value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
