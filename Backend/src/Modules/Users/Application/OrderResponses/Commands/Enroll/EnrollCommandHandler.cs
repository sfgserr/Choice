using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.Users;

namespace Users.Application.OrderResponses.Commands.Enroll
{
    internal class EnrollCommandHandler : ICommandHandler<EnrollCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;

        internal EnrollCommandHandler(IUsersDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Execute(EnrollCommand command)
        {
            var response = await _dbContext.OrderResponses.Get(r => 
                r.Id.Equals(new OrderResponseId(command.ResponseId)));
            
            if (response.Prepayment > 0)
                response.EnrollWithPrepayment(_userContext.ClientId);
            else
                response.Enroll(_userContext.ClientId);
        }
    }
}