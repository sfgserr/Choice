using BuildingBlocks.Application.Cqrs.Queries;

namespace Payments.Application.Subscriptions.Queries.CheckActiveSubscription
{
    public class CheckActiveSubscriptionQuery : IQuery<bool>
    {
        public CheckActiveSubscriptionQuery(Guid subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public Guid SubscriberId { get; }
    }
}