using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.SendReview
{
    public class SendReviewUseCasePresenter : IOutputPort
    {
        public Review Review { get; set; }

        public void Ok(Review review)
        {
            Review = review;
        }
    }
}
