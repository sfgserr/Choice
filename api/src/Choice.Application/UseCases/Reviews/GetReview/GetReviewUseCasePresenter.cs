using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.GetReview
{
    public class GetReviewUseCasePresenter : IOutputPort
    {
        public bool IsNotFound { get; set; } = false;
        public Review Review { get; set; }

        public void NotFound()
        {
            IsNotFound = true;
        }

        public void Ok(Review review)
        {
            Review = review;
        }
    }
}
