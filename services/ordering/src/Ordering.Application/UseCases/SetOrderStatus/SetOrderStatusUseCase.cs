using Choice.Ordering.Application.Services;
using Choice.Ordering.Domain.OrderEntity;
using Ordering.Application.Services;

namespace Choice.Ordering.Application.UseCases.SetOrderStatus
{
    public sealed class SetOrderStatusUseCase : ISetOrderStatusUseCase
    {
        private readonly Notification _notification;
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public SetOrderStatusUseCase(Notification notification, IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _notification = notification;
            _repository = repository;
            _unitOfWork = unitOfWork;

            _outputPort = new SetOrderStatusPresenter();
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
            order.SetOrderStatus(OrderStatus.Finished);

            _repository.Update(order);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
