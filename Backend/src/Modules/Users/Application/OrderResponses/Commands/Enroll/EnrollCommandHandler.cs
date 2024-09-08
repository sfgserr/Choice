using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
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
            var response = await _dbContext.OrderResponses.FindAsync(new OrderResponseId(command.ResponseId));

            if (response == null) throw new InvalidCommandException(["OrderResponse is not found"]);
            
            if (response.Prepayment > 0)
                response.EnrollWithPrepayment(new(_userContext.Id.Value));
            else
                response.Enroll(new(_userContext.Id.Value));
        }
    }
}