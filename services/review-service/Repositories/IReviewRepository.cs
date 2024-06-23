using Choice.ReviewService.Api.Entities;

namespace Choice.ReviewService.Api.Repositories
{
    public interface IReviewRepository
    {
        Task Add(Review review);

        Task<IList<Review>> Get(string guid);

        Task<Review> Get(int id);

        Task<bool> Update(Review review);
    }
}
