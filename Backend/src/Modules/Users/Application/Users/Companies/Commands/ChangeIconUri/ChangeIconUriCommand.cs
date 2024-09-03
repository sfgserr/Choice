using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.Users.Companies.Commands.ChangeIconUri
{
    public class ChangeIconUriCommand : ICommand
    {
        public string IconUri { get; }
    }
}