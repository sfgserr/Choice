using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.DeleteClient
{
    public class DeleteClientCommand : ICommand
    {
        public DeleteClientCommand(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; }
    }
}