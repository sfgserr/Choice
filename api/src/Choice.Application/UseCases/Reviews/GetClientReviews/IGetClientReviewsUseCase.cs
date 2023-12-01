
namespace Choice.Application.UseCases.Reviews.GetClientReviews
{
    public interface IGetClientReviewsUseCase
    {
        Task Execute(int id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
