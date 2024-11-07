using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.ChatUsers.Commands.DeleteUserCommand
{
    public class DeleteUserCommand : InternalCommandBase
    {
        public DeleteUserCommand(Guid id, Guid userId) : base(id)
        {
            UserId = userId;
        }
        
        internal Guid UserId { get; }
    }
}