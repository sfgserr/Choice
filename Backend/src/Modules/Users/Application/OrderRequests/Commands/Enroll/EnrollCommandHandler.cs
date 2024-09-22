using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests;

namespace Users.Application.OrderRequests.Commands.Enroll
{
    internal class EnrollCommandHandler : ICommandHandler<EnrollCommand>
    {
        private readonly IUsersDbContext _dbContext;

        internal EnrollCommandHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(EnrollCommand command)
        {
            var request = await _dbContext.OrderRequests.Get(r => 
                r.Id.Equals(new OrderRequestId(command.RequestId)));

            request.Enroll();
        }
    }
}