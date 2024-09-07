using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.OrderRequests;
using Users.Domain.OrderResponses;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.OrderResponses.Commands.Response
{
    internal class ResponseCommandHandler : ICommandHandler<ResponseCommand>
    {
        private readonly IOrderRequestRepository _requestRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserContext _userContext;
        private readonly IOrderResponseRepository _responseRepository;
        
        internal ResponseCommandHandler(
            IOrderRequestRepository requestRepository, 
            ICompanyRepository companyRepository, 
            IUserContext userContext, IOrderResponseRepository responseRepository)
        {
            _requestRepository = requestRepository;
            _companyRepository = companyRepository;
            _userContext = userContext;
            _responseRepository = responseRepository;
        }

        public async Task Execute(ResponseCommand command)
        {
            var orderRequest = await _requestRepository.Get(new(command.RequestId));

            var company = await _companyRepository.Get(new(_userContext.Id.Value));
            
            var orderResponse = orderRequest.Response(
                company,
                command.Price,
                command.Deadline,
                command.EnrollmentDate,
                command.Prepayment);

            await _responseRepository.Add(orderResponse);
        }
    }
}