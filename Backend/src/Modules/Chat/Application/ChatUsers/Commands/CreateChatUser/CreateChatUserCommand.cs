using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.ChatUsers.Commands.CreateChatUser
{
    public class CreateChatUserCommand : InternalCommandBase
    {
        public CreateChatUserCommand(Guid id, Guid userId, string name, string deviceName, string deviceToken) : base(id)
        {
            UserId = userId;
            Name = name;
            DeviceName = deviceName;
            DeviceToken = deviceToken;
        }

        internal Guid UserId { get; }
        
        internal string Name { get; }

        internal string DeviceName { get; }
        
        internal string DeviceToken { get; }
    }
}
