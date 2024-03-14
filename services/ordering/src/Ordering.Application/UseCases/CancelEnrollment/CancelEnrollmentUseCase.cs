using Choice.Application.Services;
using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.CancelEnrollment
{
    public class CancelEnrollmentUseCase : ICancelEnrollmentUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Notification _notification;
        private readonly IUserService _userService;

        private IOutputPort _outputPort;

        public CancelEnrollmentUseCase(IOrderRepository repository, IUnitOfWork unitOfWork, Notification notification, IUserService userService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _notification = notification;
            _userService = userService;

            _outputPort = new CancelEnrollmentPresenter();
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
                _notification.Add(nameof(order), "You are not enrolled yet");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await CancelEnrollment(order);

            _outputPort.Ok(order);
        }

        private async Task CancelEnrollment(Order order)
        {
            order.CancelEnrollment();

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
