using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.Users;

namespace Users.Application.OrderResponses.Commands.ChangeEnrollmentDate
{
    internal class ChangeEnrollmentDateCommandHandler : ICommandHandler<ChangeEnrollmentDateCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;

        internal ChangeEnrollmentDateCommandHandler(IUsersDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Execute(ChangeEnrollmentDateCommand command)
        {
            var response = await _dbContext.OrderResponses.Get(r => 
                r.Id.Equals(new OrderResponseId(command.ResponseId)));

            var userId = _userContext.Id.Value;

            if (_userContext.Role.Equals(UserRole.Client))
                response.ChangeEnrollmentDateByClient(new(userId), command.EnrollmentDate);
            else
                response.ChangeEnrollmentDateByCompany(new(userId), command.EnrollmentDate);
        }
    }
}