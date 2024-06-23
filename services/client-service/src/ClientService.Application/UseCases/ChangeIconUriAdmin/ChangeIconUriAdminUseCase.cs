using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeIconUriAdmin
{
    public sealed class ChangeIconUriAdminUseCase : IChangeIconUriAdminUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public ChangeIconUriAdminUseCase(IClientRepository repository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _userService = userService;
            _unitOfWork = unitOfWork;

            _outputPort = new ChangeIconUriAdminPresenter();
        }

        public async Task Execute(string id, string iconUri)
        {
            Client? client = await _repository.Get(id);

            if (client is null)
            {
                _outputPort.NotFound();
                return;
            }

            await ChangeIconUri(client, iconUri);

            _outputPort.Ok(client);
        }

        private async Task ChangeIconUri(Client client, string iconUri)
        {
            client.ChangeIconUri(iconUri);

            _repository.Update(client);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
