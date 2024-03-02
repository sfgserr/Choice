﻿using Choice.ClientService.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetOrderRequests
{
    public sealed class GetOrderRequestsUseCase : IGetOrderRequestsUseCase
    {
        private readonly IAddressService _addressService;
        private readonly IClientRepository _repository;

        private IOutputPort _outputPort;

        public GetOrderRequestsUseCase(IAddressService addressService, IClientRepository repository)
        {
            _addressService = addressService;
            _repository = repository;

            _outputPort = new GetOrderRequestsPresenter();
        }

        public async Task Execute()
        {
            Address address = await _addressService.GetAddress();

            IList<OrderRequest> requests = await _repository.GetRequests();

            IList<OrderRequest> requestsInRadius = await GetRequests(address, requests);

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
