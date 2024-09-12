using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.Users;

namespace Users.Application.OrderResponses.Commands.Cancel
{
    internal class CancelCommandHandler : ICommandHandler<CancelCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;
        
        internal CancelCommandHandler(IUsersDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Execute(CancelCommand command)
        {
            var response = await _dbContext.OrderResponses.Get(r => 
                r.Id.Equals(new OrderResponseId(command.ResponseId)));
            
            response.Cancel(_userContext.Id);
        }
    }
}