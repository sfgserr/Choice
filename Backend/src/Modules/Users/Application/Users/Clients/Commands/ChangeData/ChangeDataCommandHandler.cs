using BuildingBlocks.Application.Cqrs.Commands;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Clients;

namespace Users.Application.Users.Clients.Commands.ChangeData
{
    internal class ChangeDataCommandHandler : ICommandHandler<ChangeDataCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserContext _userContext;
        private readonly IUsersCounter _usersCounter;
        private readonly IGeoService _geoService;

        internal ChangeDataCommandHandler(
            IClientRepository clientRepository,
            IUserContext userContext,
            IUsersCounter usersCounter,
            IGeoService geoService)
        {
            _clientRepository = clientRepository;
            _userContext = userContext;
            _usersCounter = usersCounter;
            _geoService = geoService;
        }

        public async Task Execute(ChangeDataCommand command)
        {
            Client client = await _clientRepository.Get(new(_userContext.Id.Value));

            string[] coords = await _geoService.GetCoords(command.City, command.Street);

            client.ChangeData(
                command.Name,
                command.Email,
                command.PhoneNumber,
                new(command.City, command.Street, new(coords[0], coords[1])),
                _usersCounter);
        }
    }
}
