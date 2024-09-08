using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

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
            var orderRequest = await _dbContext.OrderRequests.FindAsync(new OrderResponseId(command.RequestId));

            var company = await _dbContext.Companies.FindAsync(_userContext.Id.Value);

            if (orderRequest == null || company == null)
                throw new InvalidCommandException(["OrderRequest or Company are not found"]);
            
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