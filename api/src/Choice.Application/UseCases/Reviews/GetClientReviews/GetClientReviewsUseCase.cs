using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.GetClientReviews
{
    public class GetClientReviewsUseCase : IGetClientReviewsUseCase
    {
        private readonly IRepository<Review> _reviewRepository;

        private IOutputPort _outputPort;

        public GetClientReviewsUseCase(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;

            _outputPort = new GetClientReviewsPresenter();
        }

        public async Task Execute(int id)
        {
            IList<Review> reviews = await _reviewRepository.Get();

            List<Review> sortedReviews = reviews.Where(r => r.Client.Id == id).ToList();

            _outputPort.Ok(sortedReviews);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
