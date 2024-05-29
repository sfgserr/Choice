using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetRequest
{
    public sealed class GetRequestUseCase : IGetRequestUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUserService _userService;

        private IOutputPort _outputPort;

        public GetRequestUseCase(IClientRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;

            _outputPort = new GetRequestPresenter();
        }

        public async Task Execute(int id)
        {
            IList<OrderRequest> requests = await _repository.GetRequests();

            OrderRequest? request = requests.FirstOrDefault(r => r.Id == id);

            bool isUserCompany = false;

            if (request is not null)
            {
                if (_userService.GetUserType() == "Company")
                {
                    isUserCompany = true;

                    string userId = _userService.GetUserId();

                    request.CompanyWatched(userId);

                    _repository.Update(request.Client!);
                }

                _outputPort.Ok(request, isUserCompany);
            }

            _outputPort.NotFound();
        }

        public void SetOutputPort(IOutputPort outputPort) 
        {
            _outputPort = outputPort;
        }
    }
}
