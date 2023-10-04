using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.GetReview
{
    public class GetReviewUseCase : IGetReviewUseCase
    {
        private readonly IRepository<Review> _reviewRepository;

        private IOutputPort _outputPort;

        public GetReviewUseCase(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;

            _outputPort = new GetReviewUseCasePresenter();
        }

        public async Task Execute(GetReviewBy getReviewBy, int id)
        {
            Func<Review, bool> func = getReviewBy switch
            {
                GetReviewBy.Company => (r) => r.Company.Id == id,
                GetReviewBy.Client => (r) => r.Client.Id == id,
                _ => throw new ArgumentException()
            };

            Review? review = await _reviewRepository.GetBy(func);

            if (review != null)
            {
                _outputPort.Ok(review);
                return;
            }

            _outputPort.NotFound();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
