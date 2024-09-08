using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests;

namespace Users.Application.OrderRequests.Commands.SetStatus
{
    internal class SetStatusCommandHandler : ICommandHandler<SetStatusCommand>
    {
        private readonly IUsersDbContext _dbContext;

        internal SetStatusCommandHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(SetStatusCommand command)
        {
            var request = await _dbContext.OrderRequests.Get(r => 
                r.Equals(new OrderRequestId(command.RequestId)));
            
            request.SetStatus(OrderStatus.Parse(command.OrderStatus));
        }
    }
}