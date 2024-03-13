using Choice.Application.Services;
using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.GetOrders
{
    public sealed class GetOrdersUseCase : IGetOrdersUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly IUserService _userService;

        private IOutputPort _outputPort;

        public GetOrdersUseCase(IOrderRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;

            _outputPort = new GetOrdersPresenter();
        }

        public async Task Execute()
        {
            string id = _userService.GetUserId();

            IList<Order> orders = await _repository.GetOrders();

            _outputPort.Ok(orders.Where(o => o.SenderId == id || o.ReceiverId == id).ToList());
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
