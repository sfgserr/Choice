using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClients
{
    public class GetClientsUseCase : IGetClientsUseCase
    {
        private readonly IRepository<Client> _clientRepository;

        private IOutputPort _outputPort;

        public GetClientsUseCase(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;

            _outputPort = new GetClientPresenter();
        }

        public async Task Execute()
        {
            await GetClients();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        private async Task GetClients()
        {
            IList<Client> clients = await _clientRepository.Get();

            _outputPort.Ok(clients);
        }
    }
}
