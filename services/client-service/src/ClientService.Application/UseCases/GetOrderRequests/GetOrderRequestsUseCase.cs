using Choice.ClientService.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetOrderRequests
{
    public sealed class GetOrderRequestsUseCase : IGetOrderRequestsUseCase
    {
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;
        private readonly IClientRepository _repository;

        private IOutputPort _outputPort;

        public GetOrderRequestsUseCase(IAddressService addressService, IClientRepository repository, IUserService userService)
        {
            _addressService = addressService;
            _repository = repository;
            _userService = userService;

            _outputPort = new GetOrderRequestsPresenter();
        }

        public async Task Execute()
        {
            string id = _userService.GetUserId();

            Client client = await _repository.Get(id);

            if (client is null)
            {
                _outputPort.NotFound();
                return;
            }

            IList<OrderRequest> requests = await _repository.GetRequests();

            IList<OrderRequest> requestsInRadius = await GetRequests(client.Address, requests);

            if (requestsInRadius.Count == 0)
            {
                _outputPort.NotFound();
                return;
            }

            _outputPort.Ok(requestsInRadius);
        }

        public async Task<IList<OrderRequest>> GetRequests(Address address, IList<OrderRequest> requests)
        {
            List<OrderRequest> requestsInRadius = new();

            foreach (OrderRequest request in requests)
            {
                int distance = await _addressService.GetDistance(address, request.Client?.Address!);

                if (distance <= request.SearchRadius)
                    requestsInRadius.Add(request);
            }

            return requestsInRadius;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
