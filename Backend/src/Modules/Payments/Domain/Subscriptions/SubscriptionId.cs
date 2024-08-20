using BuildingBlocks.Domain;

namespace Payments.Domain.Subscriptions
{
    public class SubscriptionId : TypedIdValueBase
    {
        public SubscriptionId(Guid value) : base(value)
        {

        }
    }
}
