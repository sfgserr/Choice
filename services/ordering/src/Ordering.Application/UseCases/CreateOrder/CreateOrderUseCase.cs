using Choice.Application.Services;
using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.CreateOrder
{
    public sealed class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IUserService _userService;
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public CreateOrderUseCase(IUserService userService, IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _repository = repository;
            _unitOfWork = unitOfWork;

            _outputPort = new CreateOrderPresenter();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(string receiverId, int orderRequestId, double price, double prepayment,
            int deadline, DateTime enrollmentTime)
        {
            string userId = _userService.GetUserId();

            if (userId == receiverId)
            {
                _outputPort.Invalid();
                return;
            }

            Order order = await CreateOrder(userId, receiverId, orderRequestId, price, prepayment, deadline,
                enrollmentTime);

            _outputPort.Ok(order);
        }

        private async Task<Order> CreateOrder(string userId, string receiverId, int orderRequestId, double price,
            double prepayment, int deadline, DateTime enrollmentDate)
        {
            Order order = new(orderRequestId, userId, receiverId, price, prepayment, deadline, enrollmentDate);

            await _repository.Add(order);
            await _unitOfWork.SaveChanges();

            return order;
        }
    }
}
