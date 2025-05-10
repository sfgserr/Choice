using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Reviews
{
    public class ReviewId : TypedIdValueBase
    {
        public ReviewId(Guid value) : base(value)
        {
        }
    }
}