
namespace Choice.Application.UseCases.Reviews.GetReview
{
    public enum GetReviewBy
    {
        Company = 0,
        Client = 1
    }

    public interface IGetReviewUseCase
    {
        Task Execute(GetReviewBy getReviewBy, int id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
