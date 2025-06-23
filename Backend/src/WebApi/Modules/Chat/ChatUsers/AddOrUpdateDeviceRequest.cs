namespace WebApi.Modules.Chat.ChatUsers
{
    public class AddOrUpdateDeviceRequest
    {
        public AddOrUpdateDeviceRequest(string device, string token)
        {
            Device = device;
            Token = token;
        }

        public string Device { get; }

        public string Token { get; }
    }
}