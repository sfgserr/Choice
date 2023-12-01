using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.UpdateOrder
{
    public class UpdateOrderUseCase : IUpdateOrderUseCase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public UpdateOrderUseCase(IRepository<Order> orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new UpdateOrderPresenter();
        }

        public async Task Execute(Order order) =>
            await UpdateOrder(order);

        private async Task UpdateOrder(Order order)
        {
            Order updatedOrder = await _orderRepository.Update(order);

            await _unitOfWork.Save();

            _outputPort.Ok(updatedOrder);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
