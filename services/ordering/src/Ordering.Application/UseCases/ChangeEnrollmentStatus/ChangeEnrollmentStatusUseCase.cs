using Choice.Ordering.Application.Services;
using Choice.Ordering.Domain.OrderAggregate;
using Ordering.Application.Services;

namespace Choice.Ordering.Application.UseCases.ChangeEnrollmentStatus
{
    public sealed class ChangeEnrollmentStatusUseCase : IChangeEnrollmentStatusUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public ChangeEnrollmentStatusUseCase(IOrderRepository repository, IUnitOfWork unitOfWork, Notification notification)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _notification = notification;

            _outputPort = new ChangeEnrollmentStatusPresenter();
        }

        public async Task Execute(int orderId)
        {
            Order order = await _repository.GetOrder(orderId);

            if (order is null)
            {
                _outputPort.NotFound();
                return;
            }

            if (order.IsCanceled)
            {
                _notification.Add(nameof(order), "Order is canceled");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await ChangeEnrollmentStatus(order);

            _outputPort.Ok(order);
        }

        private async Task ChangeEnrollmentStatus(Order order)
        {
            order.ChangeEnrollmentStaus();

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
