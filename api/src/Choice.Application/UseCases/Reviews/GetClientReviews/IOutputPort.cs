using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.GetClientReviews
{
    public interface IOutputPort
    {
        void Ok(IList<Review> review);
    }
}
