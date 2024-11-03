using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.ChatUsers.Commands.ChangeName
{
    public class ChangeNameCommand : InternalCommandBase
    {
        public ChangeNameCommand(Guid id, Guid userId, string name) : base(id)
        {
            UserId = userId;
            Name = name;
        }

        internal Guid UserId { get; }
        
        internal string Name { get; }
    }
}