using Grpc.Core;
using Choice.Ordering.Grpc.Protos;
using Choice.Ordering.Domain.OrderEntity;
using Choice.Application.Services;

namespace Choice.Ordering.Grpc.Services
{
    public class OrderingService : OrderingProtoService.OrderingProtoServiceBase
    {
        private readonly IOrderRepository _repository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public OrderingService(IOrderRepository repository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public override async Task<CanSendReviewResponse> CanSendReview(CanSendReviewRequest request,
            ServerCallContext context)
        {
            CanSendReviewResponse response = new();

            string id = _userService.GetUserId();

            IList<Order> orders = await _repository.GetOrders();

            orders = orders.Where(o => 
                (o.SenderId == id || o.ReceiverId == id) && o.Status == OrderStatus.Finished).ToList();

            Order order = orders.Where(o => 
                (o.SenderId == request.Guid || o.ReceiverId == request.Guid) && !o.Reviews.Contains(id)).First();

            if (order is null)
            {
                response.Id = 0;
                response.Result = false;
            }
            else
            {
                response.Id = order.Id;
                response.Result = true;

                await UpdateOrder(order, id);
            }

            return response;
        }

        private async Task UpdateOrder(Order order, string id)
        {
            order.AddReview(id);

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        }
    }
}