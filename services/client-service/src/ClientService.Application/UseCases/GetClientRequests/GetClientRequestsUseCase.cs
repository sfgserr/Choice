using Choice.ClientService.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClientRequests
{
    public sealed class GetClientRequestsUseCase : IGetClientRequestsUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUserService _userService;

        private IOutputPort _outputPort;

        public GetClientRequestsUseCase(IClientRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;

            _outputPort = new GetClientRequestsPresenter();
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

            _outputPort.Ok(client.Requests.ToList());
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
