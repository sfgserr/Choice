using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClient
{
    public sealed class GetClientUseCase : IGetClientUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUserService _userService;

        private IOutputPort _outputPort;

        public GetClientUseCase(IClientRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;

            _outputPort = new GetClientPresenter();
        }

        public void SetOutputPort(IOutputPort outpurPort)
        {
            _outputPort = outpurPort;
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

            _outputPort.Ok(client);
        }
    }
}
