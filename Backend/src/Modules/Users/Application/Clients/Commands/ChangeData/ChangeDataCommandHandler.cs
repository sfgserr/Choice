using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using BuildingBlocks.Application.GeoCoding;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Application.Clients.Commands.ChangeData
{
    internal class ChangeDataCommandHandler : ICommandHandler<ChangeDataCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IUsersCounter _usersCounter;
        private readonly IGeoCodingService _geoCodingService;

        internal ChangeDataCommandHandler(
            IUsersDbContext dbContext,
            IUserContext userContext,
            IUsersCounter usersCounter,
            IGeoCodingService geoCodingService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _usersCounter = usersCounter;
            _geoCodingService = geoCodingService;
        }

        public async Task Execute(ChangeDataCommand command)
        {
            var client = await _dbContext.Clients.Get(c => c.Id.Equals(_userContext.ClientId));

            var coords = await _geoCodingService.GetCoords(command.City, command.Street);
            
            client.ChangeData(
                command.Name,
                command.Email,
                command.PhoneNumber,
                new(command.City, command.Street, new(coords[1], coords[0])),
                _usersCounter);
        }
    }
}
