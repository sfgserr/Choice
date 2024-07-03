using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;

namespace ClientService.Application.UseCases.DeleteClientAdmin
{
    public sealed class DeleteClientAdminUseCase : IDeleteClientAdminUseCase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public DeleteClientAdminUseCase(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new DeleteClientAdminPresenter();
        }

        public async Task Execute(string id)
        {
            await _clientRepository.Delete(id);

            await _unitOfWork.SaveChanges();

            _outputPort.Ok(id);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
