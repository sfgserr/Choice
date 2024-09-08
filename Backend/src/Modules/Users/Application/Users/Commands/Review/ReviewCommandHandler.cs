using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Application.Users.Commands.Review
{
    internal class ReviewCommandHandler : ICommandHandler<ReviewCommand>
    {
        private readonly IUsersDbContext _dbContext;

        internal ReviewCommandHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(ReviewCommand command)
        {
            var user = await _dbContext.Users.FindAsync(new UserId(command.ToUserId));

            if (user == null) throw new InvalidCommandException(["User is not found"]);
            
            user.Review(command.Grade);
        }
    }
}