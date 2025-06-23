using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.GeoCoding;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Clients;

namespace Users.Application.Clients.Commands.CreateClient
{
    internal class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IGeoCodingService _geoCodeService;
        private readonly IUsersCounter _usersCounter;

        internal CreateClientCommandHandler(
            IUsersDbContext dbContext, 
            IGeoCodingService geoCodeService, 
            IUsersCounter usersCounter)
        {
            _dbContext = dbContext;
            _geoCodeService = geoCodeService;
            _usersCounter = usersCounter;
        }

        public async Task Execute(CreateClientCommand command)
        {
            var coords = await _geoCodeService.GetCoords(command.City, command.Street);

            var client = Client.Create(
                command.Name,
                command.Email,
                command.PhoneNumber,
                command.Password,
                command.DeviceName,
                command.DeviceToken,
                new(command.City, command.Street, new(coords[1], coords[0])),
                _usersCounter);

            await _dbContext.Clients.AddAsync(client);
        }
    }
}
