namespace Choice.ReviewService.Api.Infrastructure.Ordering
{
    public interface IOrderingService
    {
        Task<bool> CanSendReview(string guid);
    }
}
