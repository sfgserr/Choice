using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.ChatUsers.Commands.AddOrUpdateDevice
{
    public class AddOrUpdateDeviceCommand : ICommand
    {
        public AddOrUpdateDeviceCommand(string deviceName, string deviceToken)
        {
            DeviceName = deviceName;
            DeviceToken = deviceToken;
        }

        public string DeviceName { get; }
        
        public string DeviceToken { get; }
    }
}