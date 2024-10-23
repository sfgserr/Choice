using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.ChatUsers.Commands.CreateChatUser
{
    public class CreateChatUserCommand : InternalCommandBase
    {
        public CreateChatUserCommand(Guid id, Guid userId) : base(id)
        {
            UserId = userId;
        }

        internal Guid UserId { get; }
    }
}
