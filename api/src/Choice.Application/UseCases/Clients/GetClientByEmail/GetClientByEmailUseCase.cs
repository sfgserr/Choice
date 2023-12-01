using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.GetClientByEmail
{
    public class GetClientByEmailUseCase : IGetClientByEmailUseCase
    {
        private readonly IRepository<Client> _clientRepository;

        private IOutputPort _outputPort;

        public GetClientByEmailUseCase(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;

            _outputPort = new GetClientByEmailPresenter();
        }

        public async Task Execute(string email)
        {
            Client client = await _clientRepository.GetBy(c => c.Email == email);

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
