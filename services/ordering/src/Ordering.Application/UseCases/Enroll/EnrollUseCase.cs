using Choice.Ordering.Application.Services;
using Choice.Ordering.Domain.OrderEntity;
using Ordering.Application.Services;

namespace Choice.Ordering.Application.UseCases.Enroll
{
    public sealed class EnrollUseCase : IEnrollUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public EnrollUseCase(IOrderRepository repository, IUnitOfWork unitOfWork, Notification notification)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _notification = notification;

            _outputPort = new EnrollPresenter();
        }

        public async Task Execute(int orderId)
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
