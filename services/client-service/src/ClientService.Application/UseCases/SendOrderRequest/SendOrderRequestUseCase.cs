using Choice.ClientService.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.SendOrderRequest
{
    public sealed class SendOrderRequestUseCase : ISendOrderRequestUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public SendOrderRequestUseCase(IClientRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;

            _outputPort = new SendOrderRequestPresenter();
        }

        public async Task Execute(int clientId, string description, List<string> categories, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            Client client = await _repository.Get(clientId);

            if (client is null)
            {
                _outputPort.NotFound();
                return;
            }

            OrderRequest request = new
                (clientId,
                 categories, 
                 description, 
                 toKnowPrice, 
                 toKnowDeadline, 
                 toKnowDeadline);

            client.SendRequest(request);

            await _repository.Update(request);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
