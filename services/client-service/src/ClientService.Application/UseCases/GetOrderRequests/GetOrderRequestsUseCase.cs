using Choice.Application.Services;
using Choice.Common.ValueObjects;
using Choice.ClientService.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetOrderRequests
{
    public sealed class GetOrderRequestsUseCase : IGetOrderRequestsUseCase
    {
        private readonly IAddressService _addressService;
        private readonly ICompanyService _companyService;
        private readonly IClientRepository _repository;

        private IOutputPort _outputPort;

        public GetOrderRequestsUseCase(IAddressService addressService, IClientRepository repository, ICompanyService companyService)
        {
            _addressService = addressService;
            _repository = repository;
            _companyService = companyService;

            _outputPort = new GetOrderRequestsPresenter();
        }

        public async Task Execute(int[] categoriesId)
        {
            Address address = _companyService.GetAddress();

            string coords = await _addressService.Geocode(address);

            IList<OrderRequest> requests = await _repository.GetRequests();

            IList<OrderRequest> requestsInRadius = await GetRequests(coords, requests);

            _outputPort.Ok(requestsInRadius.Where(r => categoriesId.Any(id => id == r.CategoryId)).ToList());
        }

        public async Task<IList<OrderRequest>> GetRequests(string coords, IList<OrderRequest> requests)
        {
            List<OrderRequest> requestsInRadius = [];

            foreach (OrderRequest request in requests)
            {
                int distance = await _addressService.GetDistance(coords, request.Client?.Coordinates!);

                if (distance <= request.SearchRadius && request.Status == OrderStatus.Active)
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
