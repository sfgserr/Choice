using Choice.Application.Services;
using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.ConfirmEnrollmentDate
{
    public sealed class ConfirmEnrollmentDateUseCase : IConfirmEnrollmentDateUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public ConfirmEnrollmentDateUseCase(IOrderRepository repository, IUnitOfWork unitOfWork, Notification notification)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _notification = notification;

            _outputPort = new ConfirmEnrollmentDatePresenter();
        }

        public async Task Execute(int id)
        {
            Order order = await _repository.GetOrder(id);

            if (order is null)
            {
                _outputPort.NotFound();
                return;
            }

            if (order.Status != OrderStatus.Active)
            {
                _notification.Add(nameof(order), "Order is not active");
            }

            if (order.IsDateConfirmed)
            {
                _notification.Add(nameof(order), "Enrollment date already confirmed");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await ConfirmEnrollmentDate(order);

            _outputPort.Ok(order);
        }

        private async Task ConfirmEnrollmentDate(Order order)
        {
            order.ConfirmDate();

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
