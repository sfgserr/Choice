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

        public async Task<CanSendReviewResponse> CanSendReview(string guid) => 
            await _orderingService.CanSendReviewAsync(new() { Guid = guid });
    }
}
