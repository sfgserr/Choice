using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Identity.Application.Contracts;
using Identity.Domain.Users;

namespace Identity.Application.Users.ChangeData
{
    internal class ChangeDataCommandHandler : ICommandHandler<ChangeDataCommand>
    {
        private readonly IIdentityDbContext _dbContext;

        internal ChangeDataCommandHandler(IIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(ChangeDataCommand command)
        {
            var user = await _dbContext.Users.Get(u => u.Id.Equals(new UserId(command.UserId)));
            
            user.ChangeData(
                command.Email,
                command.PhoneNumber,
                new(
                    command.City, 
                    command.Street, 
                    new(
                        command.Latitude, 
                        command.Longitude)));
        }
    }
}