using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.GeoCoding;
using Users.Domain.Users;
using Users.Domain.Users.Clients;

namespace Users.Application.Users.Clients.Commands.CreateClient
{
    internal class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IGeoService _geoService;
        private readonly IUsersCounter _usersCounter;

        internal CreateClientCommandHandler(
            IClientRepository clientRepository, 
            IGeoService geoService, 
            IUsersCounter usersCounter)
        {
            _clientRepository = clientRepository;
            _geoService = geoService;
            _usersCounter = usersCounter;
        }

        public async Task Execute(CreateClientCommand command)
        {
            string[] coords = await _geoService.GetCoords(command.City, command.Street);

            Client client = Client.Create(
                command.Name,
                command.Email,
                command.PhoneNumber,
                PasswordManager.HashPassword(command.Password),
                new(command.City, command.Street, new(coords[0], coords[1])),
                _usersCounter);

            await _clientRepository.Add(client);
        }
    }
}
