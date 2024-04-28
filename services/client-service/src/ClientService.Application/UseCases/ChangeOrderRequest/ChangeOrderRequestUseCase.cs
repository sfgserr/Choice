using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.ChangeOrderRequest
{
    public sealed class ChangeOrderRequestUseCase : IChangeOrderRequestUseCase
    {
        private readonly IUserService _userService;
        private readonly IClientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public ChangeOrderRequestUseCase(IUserService userService, IClientRepository repository, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _repository = repository;
            _unitOfWork = unitOfWork;

            _outputPort = new ChangeOrderRequestPresenter();
        }

        public async Task Execute(int id, string description, List<string> photoUris, int categoryId, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            string userId = _userService.GetUserId();

            Client client = await _repository.Get(userId);
            
            if (client is null)
            {
                _outputPort.NotFound();
                return;
            }

            OrderRequest? request = client.Requests.FirstOrDefault(r => r.Id == id);

            if (request is null)
            {
                _outputPort.NotFound();
                return;
            }

            request.Update
                (description, 
                 photoUris, 
                 categoryId, 
                 searchRadius, 
                 toKnowPrice, 
                 toKnowDeadline, 
                 toKnowEnrollmentDate);

            _repository.Update(client);

            await _unitOfWork.SaveChanges();

            _outputPort.Ok(request);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
