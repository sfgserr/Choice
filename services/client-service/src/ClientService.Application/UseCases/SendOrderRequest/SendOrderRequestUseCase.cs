using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.SendOrderRequest
{
    public sealed class SendOrderRequestUseCase : ISendOrderRequestUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        private IOutputPort _outputPort;

        public SendOrderRequestUseCase(IClientRepository repository, IUnitOfWork unitOfWork, IUserService userService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userService = userService;

            _outputPort = new SendOrderRequestPresenter();
        }

        public async Task Execute(string description, List<string> photoUris, int categoryId, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            string id = _userService.GetUserId();

            Client client = await _repository.Get(id);

            if (client is null)
            {
                _outputPort.NotFound();
                return;
            }

            OrderRequest request = new
                (client.Id,
                 categoryId,
                 description,
                 photoUris,
                 toKnowPrice,
                 toKnowDeadline,
                 toKnowEnrollmentDate,
                 searchRadius,
                 []);

            client.SendRequest(request);

            await _repository.Update(request);

            await _unitOfWork.SaveChanges();

            _outputPort.Ok(request);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
