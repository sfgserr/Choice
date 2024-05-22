using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetRequest
{
    public sealed class GetRequestUseCase : IGetRequestUseCase
    {
        private readonly IClientRepository _repository;

        private IOutputPort _outputPort;

        public GetRequestUseCase(IClientRepository repository)
        {
            _repository = repository;

            _outputPort = new GetRequestPresenter();
        }

        public async Task Execute(int id)
        {
            IList<OrderRequest> requests = await _repository.GetRequests();

            OrderRequest? request = requests.FirstOrDefault(r => r.Id == id);

            if (request is not null)
            {
                _outputPort.Ok(request);
            }

            _outputPort.NotFound();
        }

        public void SetOutputPort(IOutputPort outputPort) 
        {
            _outputPort = outputPort;
        }
    }
}
