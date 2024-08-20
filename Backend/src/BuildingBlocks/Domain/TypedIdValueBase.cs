
namespace BuildingBlocks.Domain
{
    public abstract class TypedIdValueBase : ValueObject
    {
        protected TypedIdValueBase(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
