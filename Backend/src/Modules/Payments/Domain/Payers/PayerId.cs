using BuildingBlocks.Domain;

namespace Payments.Domain.Payers
{
    public class PayerId : TypedIdValueBase
    {
        public PayerId(Guid value) : base(value)
        {

        }
    }
}
