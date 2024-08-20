using BuildingBlocks.Domain;

namespace Payments.Domain.Subscriptions
{
    public class SubscriberId : TypedIdValueBase
    {
        public SubscriberId(Guid value) : base(value)
        {

        }
    }
}
