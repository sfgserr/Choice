using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests;
using Users.Domain.Users;

namespace Users.Application.OrderResponses.Commands.Response
{
    internal class ResponseCommandHandler : ICommandHandler<ResponseCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUsersDbContext _dbContext;
        private readonly IOrderResponsesCounter _counter;
        
        internal ResponseCommandHandler( 
            IUserContext userContext,
            IUsersDbContext dbContext, IOrderResponsesCounter counter)
        {
            _userContext = userContext;
            _dbContext = dbContext;
            _counter = counter;
        }

        public async Task Execute(ResponseCommand command)
        {
            var orderRequest = await _dbContext.OrderRequests.Get(r => 
                r.Id.Equals(new OrderRequestId(command.RequestId)));

            var company = await _dbContext.Companies.Get(c => c.Id.Equals(_userContext.CompanyId));
            
            var orderResponse = orderRequest.Response(
                company,
                command.Price,
                command.Deadline,
                command.EnrollmentDate,
                command.Prepayment,
                _counter);

            await _dbContext.OrderResponses.AddAsync(orderResponse);
        }
    }
}