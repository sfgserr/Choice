using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.SendReview
{
    public class SendReviewUseCase : ISendReviewUseCase
    {
        private readonly IRepository<Review> _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public SendReviewUseCase(IRepository<Review> reviewRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new SendReviewPresenter();
        }

        public async Task Execute(Client client, Company company, int grade, List<string> photoUris, string text) =>
            await SendReview(client, company, grade, photoUris, text);

        private async Task SendReview(Client client, Company company, int grade, List<string> photoUris, string text)
        {
            Review review = new Review()
            {
                Client = client,
                Company = company,
                Grade = grade,
                PhotoUris = photoUris,
                Text = text
            };

            Review newReview = await _reviewRepository.Create(review); 

            await _unitOfWork.Save();

            _outputPort.Ok(newReview);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
