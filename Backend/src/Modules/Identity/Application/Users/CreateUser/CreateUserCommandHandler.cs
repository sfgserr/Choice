using BuildingBlocks.Application.Cqrs.Commands;
using Identity.Application.Contracts;
using Identity.Domain.Users;

namespace Identity.Application.Users.CreateUser
{
    internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IIdentityDbContext _dbContext;

        internal CreateUserCommandHandler(IIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(CreateUserCommand command)
        {
            var user = User.Create(
                new(command.UserId),
                command.Email,
                command.Password,
                command.PhoneNumber,
                UserRole.Parse(command.UserRole), 
                new(command.City, command.Street, new(command.Latitude, command.Longitude)));

            await _dbContext.Users.AddAsync(user);
        }
    }
}