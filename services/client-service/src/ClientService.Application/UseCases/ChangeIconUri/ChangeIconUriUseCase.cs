using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeIconUri
{
    public sealed class ChangeIconUriUseCase : IChangeIconUriUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public ChangeIconUriUseCase(IClientRepository repository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _userService = userService;
            _unitOfWork = unitOfWork;

            _outputPort = new ChangeIconUriPresenter();
        }

        public async Task Execute(string iconUri)
        {
            string userId = _userService.GetUserId();

            Client? client = await _repository.Get(userId);

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
