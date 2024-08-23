using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.Users;
using Users.Domain.Users.Clients;

namespace Application.Users.Clients.Commands.ChangIconUri
{
    internal class ChangeIconUriCommandHandler : ICommandHandler<ChangeIconUriCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserContext _userContext;

        internal ChangeIconUriCommandHandler(IClientRepository clientRepository, IUserContext userContext)
        {
            _clientRepository = clientRepository;
            _userContext = userContext;
        }

        public async Task Execute(ChangeIconUriCommand command)
        {
            Client client = await _clientRepository.Get(new(_userContext.Id.Value));

            client.ChangeIconUri(command.IconUri);
        }
    }
}
