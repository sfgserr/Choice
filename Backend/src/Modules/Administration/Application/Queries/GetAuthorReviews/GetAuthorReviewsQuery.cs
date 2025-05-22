using BuildingBlocks.Application.Cqrs.Queries;

namespace Administration.Application.Queries.GetAuthorReviews
{
    public class GetAuthorReviewsQuery : IQuery<IEnumerable<ReviewDto>>
    {
        public GetAuthorReviewsQuery(Guid authorId)
        {
            AuthorId = authorId;
        }

        public Guid AuthorId { get; }
    }
}