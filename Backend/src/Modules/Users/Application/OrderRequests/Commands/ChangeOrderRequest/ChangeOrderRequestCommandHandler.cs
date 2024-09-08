using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests;
using Users.Domain.Users;

namespace Users.Application.OrderRequests.Commands.ChangeOrderRequest
{
    internal class ChangeOrderRequestCommandHandler : ICommandHandler<ChangeOrderRequestCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;

        internal ChangeOrderRequestCommandHandler(IUsersDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Execute(ChangeOrderRequestCommand command)
        {
            var request = await _dbContext.OrderRequests.FindAsync(new OrderRequestId(command.RequestId));

            if (request == null) throw new InvalidCommandException(["OrderRequest is not found"]);
            
            request.Change(
                command.ToKnowPrice,
                command.ToKnowDeadline,
                command.ToKnowEnrollmentDate,
                command.Distance,
                command.PhotoUris,
                command.Description,
                new(command.CategoryId),
                new(_userContext.Id.Value));
        }
    }
}