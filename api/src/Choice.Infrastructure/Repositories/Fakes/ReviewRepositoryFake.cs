using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Infrastructure.Repositories.Fakes
{
    public class ReviewRepositoryFake : IRepository<Review>
    {
        private readonly ChoiceContextFake _context;

        public ReviewRepositoryFake(ChoiceContextFake context)
        {
            _context = context;
        }

        public async Task<Review> Create(Review entity)
        {
            _context.Reviews.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task Delete(Review entity)
        {
            Review entityToRemove = _context.Reviews.FirstOrDefault(c => c.Id == entity.Id);

            if (entityToRemove is null)
            {
                return;
            }

            _context.Reviews.Remove(entityToRemove);
            await Task.CompletedTask;
        }

        public async Task<IList<Review>> Get()
        {
            IList<Review> reviews = _context.Reviews.ToList();

            return await Task.FromResult(reviews);
        }

        public async Task<Review> GetBy(Func<Review, bool> func)
        {
            Review review = _context.Reviews.FirstOrDefault(c => func(c));

            return await Task.FromResult(review);
        }

        public async Task<Review> Update(Review entity)
        {
            Review oldReview = _context.Reviews.FirstOrDefault(c => c.Id == entity.Id);

            if (oldReview != null)
            {
                _context.Reviews.Remove(oldReview);
            }

            _context.Reviews.Add(entity);

            return await Task.FromResult(entity);
        }
    }
}
