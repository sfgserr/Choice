using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.GetReviews
{
    public class GetReviewsUseCase : IGetReviewUseCase
    {
        private readonly IRepository<Review> _reviewRepository;

        private IOutputPort _outputPort;

        public GetReviewsUseCase(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;

            _outputPort = new GetReviewsPresenter();
        }

        public async Task Execute(GetReviewBy getReviewBy, int id)
        {
            Func<Review, bool> func = getReviewBy switch
            {
                GetReviewBy.Company => (r) => r.Company.Id == id,
                GetReviewBy.Client => (r) => r.Client.Id == id,
                _ => throw new ArgumentException()
            };

            IList<Review> reviews = await _reviewRepository.Get();

            List<Review> sortedReviews = reviews.Where(func).ToList();

            _outputPort.Ok(sortedReviews);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
