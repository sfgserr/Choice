using BuildingBlocks.Application.Cqrs.Queries;

namespace Users.Application.OrderResponses.Queries.GetReviews 
{
    public class GetReviewsQuery : IQuery<IEnumerable<ReviewDto>>
    {
        public GetReviewsQuery(Guid toUserId)
        {
            ToUserId = toUserId;
        }

        public Guid ToUserId { get; }
    }
}