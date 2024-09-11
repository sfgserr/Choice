using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Application.OrderRequests.Commands.CreateOrderRequest
{
    internal class CreateOrderRequestCommandHandler : ICommandHandler<CreateOrderRequestCommand>
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

        public async Task Execute(CreateOrderRequestCommand command)
        {
            var client = await _dbContext.Clients.Get(c => c.UserId.Equals(_userContext.Id));
            
            var orderRequest = client.CreateRequest(
                command.ToKnowPrice,
                command.ToKnowDeadline,
                command.ToKnowEnrollmentDate,
                command.Distance,
                command.PhotoUris,
                command.Description,
                new(command.CategoryId));

            await _dbContext.OrderRequests.AddAsync(orderRequest);
        }
    }
}