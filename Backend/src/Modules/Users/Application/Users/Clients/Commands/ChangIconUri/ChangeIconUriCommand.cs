using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.Users.Clients.Commands.ChangIconUri
{
    public class ChangeIconUriCommand : ICommand
    {
        public ChangeIconUriCommand(string iconUri)
        {
            IconUri = iconUri;
        }

        public string IconUri { get; }
    }
}
