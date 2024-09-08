using BuildingBlocks.Application.Cqrs.Commands;
using Users.Application.Contracts;
using Users.Application.Users;
using Users.Domain.Users;
using Users.Domain.Users.Clients;

namespace Users.Application.Clients.Commands.CreateClient
{
    internal class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IGeoService _geoService;
        private readonly IUsersCounter _usersCounter;

        internal CreateClientCommandHandler(
            IUsersDbContext dbContext, 
            IGeoService geoService, 
            IUsersCounter usersCounter)
        {
            _dbContext = dbContext;
            _geoService = geoService;
            _usersCounter = usersCounter;
        }

        public async Task Execute(CreateClientCommand command)
        {
            var coords = await _geoService.GetCoords(command.City, command.Street);

            var client = Client.Create(
                command.Name,
                command.Email,
                command.PhoneNumber,
                PasswordManager.HashPassword(command.Password),
                new(command.City, command.Street, new(coords[0], coords[1])),
                _usersCounter);

            await _dbContext.Clients.AddAsync(client);
        }
    }
}
