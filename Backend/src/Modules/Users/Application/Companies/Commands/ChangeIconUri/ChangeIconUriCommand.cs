using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.Companies.Commands.ChangeIconUri
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