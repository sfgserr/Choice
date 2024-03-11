using Grpc.Core;
using Ordering.Grpc;

namespace Ordering.Grpc.Services
{
    public class OrderingService : OrderingProtoService.OrderingProtoServiceBase
    {
        private readonly ILogger<OrderingService> _logger;

        public OrderingService(ILogger<OrderingService> logger)
        {
            _logger = logger;
        }

        public override Task<CanSendReviewResponse> CanSendReview(CanSendReviewRequest request, ServerCallContext context)
        {
           
        }
    }
}