using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.GetClientReviews
{
    public class GetClientReviewsPresenter : IOutputPort
    {
        public IList<Review>? Review { get; set; }

        public void Ok(IList<Review> review)
        {
            Review = review;
        }
    }
}
