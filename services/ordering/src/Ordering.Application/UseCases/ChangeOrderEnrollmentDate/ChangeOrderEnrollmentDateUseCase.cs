using Choice.Ordering.Application.Services;
using Choice.Ordering.Domain.OrderEntity;
using Ordering.Application.Services;

namespace Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentDate
{
    public sealed class ChangeOrderEnrollmentDateUseCase : IChangeOrderEnrollmentDateUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public ChangeOrderEnrollmentDateUseCase(IOrderRepository repository, IUnitOfWork unitOfWork, Notification notification)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _notification = notification;

            _outputPort = new ChangeOrderEnrollmentDatePresenter();
        }

        public async Task Execute(int orderId, DateTime newDate)
        {
            Order order = await _repository.GetOrder(orderId);

            if (order is null)
            {
                _outputPort.NotFound();
                return;
            }

            if (order.Status == OrderStatus.Canceled)
            {
                _notification.Add(nameof(order), "Order is canceled");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await ChangeOrderEnrollmentDate(order, newDate);

            _outputPort.Ok(order);
        }

        private async Task ChangeOrderEnrollmentDate(Order order, DateTime newDate)
        {
            order.SetEnrollmentDate(newDate);

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        } 

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
