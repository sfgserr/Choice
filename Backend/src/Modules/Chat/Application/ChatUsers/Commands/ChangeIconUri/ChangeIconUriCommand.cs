using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.ChatUsers.Commands.ChangeIconUri
{
    public class ChangeIconUriCommand : InternalCommandBase
    {
        public ChangeIconUriCommand(Guid id, Guid userId, string iconUri) : base(id)
        {
            UserId = userId;
            IconUri = iconUri;
        }

        internal Guid UserId { get; }

        internal string IconUri { get; }
    }
}
