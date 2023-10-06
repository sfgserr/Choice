using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.GetOrder
{
    public class GetOrderUseCase : IGetOrderUseCase
    {
        private readonly IRepository<Order> _orderRepository;

        private IOutputPort _outputPort;

        public GetOrderUseCase(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;

            _outputPort = new GetOrderUseCasePresenter();
        }

        public async Task Execute(Func<Order, bool> func) => 
            await GetOrder(func);

        private async Task GetOrder(Func<Order, bool> func)
        {
            Order? order = await _orderRepository.GetBy(func);

            if (order != null)
            {
                _outputPort.Ok(order);
                return;
            }

            _outputPort.NotFound();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
