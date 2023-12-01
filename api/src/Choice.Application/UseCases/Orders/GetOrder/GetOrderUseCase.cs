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

            _outputPort = new GetOrderPresenter();
        }

        public async Task Execute(int id) => 
            await GetOrder(id);

        private async Task GetOrder(int id)
        {
            Order? order = await _orderRepository.GetBy(o => o.Id == id);

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
