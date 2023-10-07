
namespace Choice.Application.UseCases.Reviews.GetReviews
{
    public enum GetReviewBy
    {
        Company = 0,
        Client = 1
    }

    public interface IGetReviewsUseCase
    {
        Task Execute(GetReviewBy getReviewBy, int id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
