using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
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
            var request = await _dbContext.OrderRequests.FindAsync(new OrderRequestId(command.RequestId));

            if (request == null) throw new InvalidCommandException(["OrderRequest is not found"]);
            
            request.SetStatus(OrderStatus.Parse(command.OrderStatus));
        }
    }
}