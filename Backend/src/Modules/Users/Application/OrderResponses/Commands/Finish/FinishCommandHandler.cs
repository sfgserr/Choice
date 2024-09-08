using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.Users;

namespace Users.Application.OrderResponses.Commands.Finish
{
    internal class FinishCommandHandler : ICommandHandler<FinishCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;
        
        internal FinishCommandHandler(IUsersDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Execute(FinishCommand command)
        {
            var response = await _dbContext.OrderResponses.Get(r => 
                r.Id.Equals(new OrderResponseId(command.ResponseId)));
            
            response.Finish(_userContext.Id);
        }
    }
}