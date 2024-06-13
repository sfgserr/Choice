using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetRequest
{
    public sealed class GetRequestUseCase : IGetRequestUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public GetRequestUseCase(IClientRepository repository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _userService = userService;
            _unitOfWork = unitOfWork;

            _outputPort = new GetRequestPresenter();
        }

        public async Task Execute(int id)
        {
            IList<OrderRequest> requests = await _repository.GetRequests();

            OrderRequest? request = requests.FirstOrDefault(r => r.Id == id);

            if (request is not null)
            {
                bool isUserCompany = _userService.GetUserType() == "Company";

                if (isUserCompany)
                {
                    string userId = _userService.GetUserId();

                    request.CompanyWatched(userId);

                    _repository.Update(request.Client!);

                    await _unitOfWork.SaveChanges();
                }

                _outputPort.Ok(request, isUserCompany);
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
