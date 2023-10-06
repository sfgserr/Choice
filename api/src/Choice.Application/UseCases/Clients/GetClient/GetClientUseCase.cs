using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClient
{
    public class GetClientUseCase : IGetClientUseCase
    {
        private readonly IRepository<Client> _clientRepository;

        private IOutputPort _outputPort;

        public GetClientUseCase(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;

            _outputPort = new GetClientPresenter();
        }

        public async Task Execute(Func<Client, bool> func)
        {
            Client? client = await _clientRepository.GetBy(func);

            if (client != null)
            {
                _outputPort.Ok(client);
                return;
            }

            _outputPort.NotFound();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
