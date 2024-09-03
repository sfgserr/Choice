using BuildingBlocks.Domain;

namespace Administration.Domain.Categories
{
    public class CategoryId : ValueObject
    {
        public CategoryId(int value)
        {
            Value = value;
        }
        
        public int Value { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
