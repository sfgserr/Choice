using Choice.Ordering.Grpc.Protos;

namespace Choice.ReviewService.Api.Services
{
    public class OrderingService
    {
        private readonly OrderingProtoService.OrderingProtoServiceClient _orderingService;

        public OrderingService(OrderingProtoService.OrderingProtoServiceClient orderingService)
        {
            _orderingService = orderingService;
        }

        public async Task<bool> AddReview(string fromUserGuid, string toUserGuid)
        {
            AddReviewResponse response = await _orderingService.AddReviewAsync(
                new() { FromUserGuid = fromUserGuid, ToUserGuid = toUserGuid });

            return response.Result;
        }
    }
}
