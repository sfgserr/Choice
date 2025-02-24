using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests;
using Users.Domain.Users;

namespace Users.Application.OrderRequests.Commands.CreateOrderRequest
{
    internal class CreateOrderRequestCommandHandler : ICommandHandlerWithResult<CreateOrderRequestCommand, Guid>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;
        
        internal CreateOrderRequestCommandHandler(
            IUsersDbContext dbContext, 
            IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<Guid> Execute(CreateOrderRequestCommand command)
        {
            var client = await _dbContext.Clients.Get(c => c.Id.Equals(_userContext.ClientId));
            
            var orderRequest = client.CreateRequest(
                command.ToKnowPrice,
                command.ToKnowDeadline,
                command.ToKnowEnrollmentDate,
                command.Distance,
                command.PhotoUris,
                command.Description,
                new(command.CategoryId));

            var entry = await _dbContext.OrderRequests.AddAsync(orderRequest);

            return entry.Entity.Id.Value;
        }
    }
}