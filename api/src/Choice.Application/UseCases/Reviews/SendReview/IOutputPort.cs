using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.SendReview
{
    public interface IOutputPort
    {
        void Ok(Review review);
    }
}
