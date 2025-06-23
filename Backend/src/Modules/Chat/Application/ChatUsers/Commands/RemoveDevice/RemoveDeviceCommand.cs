using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.ChatUsers.Commands.RemoveDevice
{
    public class RemoveDeviceCommand : ICommand
    {
        public RemoveDeviceCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}