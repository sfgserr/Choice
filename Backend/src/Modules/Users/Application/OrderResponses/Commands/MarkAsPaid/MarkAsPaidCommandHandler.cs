using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests.OrderResponses;

namespace Users.Application.OrderResponses.Commands.MarkAsPaid
{
    internal class MarkAsPaidCommandHandler : ICommandHandler<MarkAsPaidCommand>
    {
        private readonly IUsersDbContext _dbContext;

        internal MarkAsPaidCommandHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(MarkAsPaidCommand command)
        {
            var response = await _dbContext.OrderResponses.Get(r => 
                r.Id.Equals(new OrderResponseId(command.Id)));

            response.MarkAsPaid();
        }
    }
}
