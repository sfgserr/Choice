using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.GetReviews
{
    public class GetReviewsUseCasePresenter : IOutputPort
    {
        public IList<Review> Review { get; set; }

        public void Ok(IList<Review> review)
        {
            Review = review;
        }
    }
}
