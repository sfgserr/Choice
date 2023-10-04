using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.GetReviews
{
    public interface IOutputPort
    {
        void Ok(IList<Review> review);
    }
}
