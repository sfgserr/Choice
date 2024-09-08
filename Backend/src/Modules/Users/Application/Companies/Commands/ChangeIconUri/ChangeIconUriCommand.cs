using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.Companies.Commands.ChangeIconUri
{
    public class ChangeIconUriCommand : ICommand
    {
        public string IconUri { get; }
    }
}