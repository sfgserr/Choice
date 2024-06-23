using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClientAdmin
{
    public sealed class GetClientAdminUseCase : IGetClientAdminUseCase
    {
        private readonly IClientRepository _repository;

        private IOutputPort _outputPort;

        public GetClientAdminUseCase(IClientRepository repository)
        {
            _repository = repository;

            _outputPort = new GetClientAdminPresenter();
        }

        public void SetOutputPort(IOutputPort outpurPort)
        {
            _outputPort = outpurPort;
        }

        public async Task Execute(string id)
        {
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
