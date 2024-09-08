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
        
        internal ResponseCommandHandler( 
            IUserContext userContext,
            IUsersDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task Execute(ResponseCommand command)
        {
            var orderRequest = await _dbContext.OrderRequests.Get(r => 
                r.Id.Equals(new OrderRequestId(command.RequestId)));

            var company = await _dbContext.Companies.Get(c => c.Id.Equals(_userContext.Id));
            
            var orderResponse = orderRequest.Response(
                company,
                command.Price,
                command.Deadline,
                command.EnrollmentDate,
                command.Prepayment);

            await _dbContext.OrderResponses.AddAsync(orderResponse);
        }
    }
}