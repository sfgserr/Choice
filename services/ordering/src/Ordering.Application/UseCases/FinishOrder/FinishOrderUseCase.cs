using Choice.Application.Services;
using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.FinishOrder
{
    public sealed class FinishOrderUseCase : IFinishOrderUseCase
    {
        private readonly Notification _notification;
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        private IOutputPort _outputPort;

        public FinishOrderUseCase(Notification notification, IOrderRepository repository, IUnitOfWork unitOfWork, IUserService userService)
        {
            _notification = notification;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userService = userService;

            _outputPort = new FinishOrderPresenter();
        }

        public async Task Execute(int orderId)
        {
            string id = _userService.GetUserId();

            Order order = await _repository.GetOrder(orderId);

            if (order is null)
            {
                _outputPort.NotFound();
                return;
            }

            if (order.ReceiverId != id && order.SenderId != id)
            {
                _notification.Add(nameof(id), "You don't have such order");
            }

            if (order.Status != OrderStatus.Active)
            {
                _notification.Add(nameof(order), "Order is not active");
            }

            if (!order.IsEnrolled)
            {
                _notification.Add(nameof(order), "You need to be enrolled to finish the order");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await FinishOrder(order);

            _outputPort.Ok(order);
        }

        private async Task FinishOrder(Order order)
        {
            order.FinishOrder();

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
