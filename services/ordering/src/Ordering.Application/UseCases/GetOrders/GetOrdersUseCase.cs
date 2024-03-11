using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.GetOrders
{
    public sealed class GetOrdersUseCase : IGetOrdersUseCase
    {
        private readonly IOrderRepository _repository;

        private IOutputPort _outputPort;

        public GetOrdersUseCase(IOrderRepository repository)
        {
            _repository = repository;

            _outputPort = new GetOrdersPresenter();
        }

        public async Task Execute(string guid)
        {
            IList<Order> orders = await _repository.GetOrders();

            _outputPort.Ok(orders.Where(o => o.SenderId == guid || o.ReceiverId == guid).ToList());
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
