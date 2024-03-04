using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClients
{
    public sealed class GetClientsUseCase : IGetClientsUseCase
    {
        private readonly IClientRepository _repository;

        private IOutputPort _outputPort;

        public GetClientsUseCase(IClientRepository repository)
        {
            _repository = repository;

            _outputPort = new GetClientsPresenter();
        }

        public async Task Execute()
        {
            IList<Client> clients = await _repository.GetAll();

            _outputPort.Ok(clients);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
