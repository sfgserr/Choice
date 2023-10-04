using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.GetReview
{
    public interface IOutputPort
    {
        void Ok(Review review);

        void NotFound();
    }
}
