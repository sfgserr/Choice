using Choice.Domain.Models;

namespace Choice.Application.UseCases.Reviews.SendReview
{
    public interface ISendReviewUseCase
    {
        Task Execute(Client client, Company company, int grade, List<string> photoUris, string text);

        void SetOutputPort(IOutputPort outputPort);
    }
}
