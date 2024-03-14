using Grpc.Core;
using Choice.Ordering.Grpc.Protos;
using Choice.Ordering.Domain.OrderEntity;
using Choice.Application.Services;

namespace Choice.Ordering.Grpc.Services
{
    public class OrderingService : OrderingProtoService.OrderingProtoServiceBase
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderingService(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public override async Task<AddReviewResponse> AddReview(AddReviewRequest request,
            ServerCallContext context)
        {
            AddReviewResponse response = new()
            {
                Result = false
            };

            IList<Order> orders = await _repository.GetOrders(request.ToUserGuid, request.FromUserGuid);

            if (orders.Count == 0)
                return response;

            Order? order = orders.FirstOrDefault(o => 
                o.Status != OrderStatus.Active && !o.Reviews.Contains(request.FromUserGuid));

            if (order is not null)
            {
                response.Result = true;

                await AddReview(order, request.FromUserGuid);
            }

            return response;
        }

        private async Task AddReview(Order order, string id)
        {
            order.AddReview(id);

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        }
    }
}