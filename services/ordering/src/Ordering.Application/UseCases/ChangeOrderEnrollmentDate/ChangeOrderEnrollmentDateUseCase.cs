using Choice.Application.Services;
using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentDate
{
    public sealed class ChangeOrderEnrollmentDateUseCase : IChangeOrderEnrollmentDateUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Notification _notification;
        private readonly IUserService _userService;

        private IOutputPort _outputPort;

        public ChangeOrderEnrollmentDateUseCase(IOrderRepository repository, IUnitOfWork unitOfWork, Notification notification, IUserService userService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _notification = notification;
            _userService = userService;

            _outputPort = new ChangeOrderEnrollmentDatePresenter();
        }

        public async Task Execute(int orderId, DateTime? newDate)
        {
            string id = _userService.GetUserId();

            Order order = await _repository.GetOrder(orderId);

            if (order is null)
            {
                _outputPort.NotFound();
                return;
            }

            if (order.ClientId != id && order.CompanyId != id)
            {
                _notification.Add(nameof(id), "You don't have such order");
            }

            if (order.UserChangedEnrollmentDateGuid == order.ClientId && id == order.ClientId)
            {
                _notification.Add(nameof(id), "Client cannot change enrollment time when date isn't confirmed");
            }

            if (order.IsEnrolled)
            {
                _notification.Add(nameof(order), "You are enrolled");
            }

            if (order.Status != OrderStatus.Active)
            {
                _notification.Add(nameof(order), "Order is not active");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await ChangeOrderEnrollmentDate(order, newDate, id);

            _outputPort.Ok(order, id != order.ClientId ? order.ClientId : order.CompanyId);
        }

        private async Task ChangeOrderEnrollmentDate(Order order, DateTime? newDate, string id)
        {
            order.SetEnrollmentDate(newDate, id);

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        } 

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
