using Choice.Application.Services;
using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.Enroll
{
    public sealed class EnrollUseCase : IEnrollUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public EnrollUseCase(IOrderRepository repository, IUnitOfWork unitOfWork, Notification notification, IUserService userService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _notification = notification;
            _userService = userService;

            _outputPort = new EnrollPresenter();
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

            if (order.ReceiverId != id)
            {
                _notification.Add(nameof(id), "You don't have such order");
            }

            if (order.Status != OrderStatus.Active)
            {
                _notification.Add(nameof(order), "Order is not active");
            }

            if (!order.IsDateConfirmed)
            {
                _notification.Add(nameof(order), "Company didn't confirm enrollment date");
            }

            if (order.IsEnrolled)
            {
                _notification.Add(nameof(order), "You are already enrolled");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await Enroll(order);

            _outputPort.Ok(order);
        }

        private async Task Enroll(Order order)
        {
            order.Enroll();

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
