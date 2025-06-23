using BuildingBlocks.Domain;

namespace Chat.Domain.ChatUsers
{
    public class ChatUser : Entity, IAggregateRoot
    {
        private string _name;

        private string _iconUri;

        private bool _isDeleted;

        private readonly List<Device> _devices = [];
        
        private ChatUser()
        {

        }

        private ChatUser(
            ChatUserId id,
            string name,
            string iconUri,
            bool isDeleted,
            List<Device> devices)
        {
            Id = id;
            
            _name = name;
            _iconUri = iconUri;
            _isDeleted = isDeleted;
            _devices = devices;
        }

        public static ChatUser Create(
            ChatUserId id,
            string name,
            string iconUri,
            string deviceName,
            string deviceToken)
        {
            return new ChatUser(id, name, iconUri, false, [Device.Create(id, deviceName, deviceToken)]);
        }

        public ChatUserId Id { get; }

        public void ChangeName(string name)
        {
            _name = name;
        }
        
        public void ChangeIconUri(string iconUri)
        {
            _iconUri = iconUri;
        }

        public void AddOrUpdateDevice(string name, string token)
        {
            var device = _devices.FirstOrDefault(d => d.Name == name);

            if (device == null)
            {
                device = Device.Create(Id, name, token);
                _devices.Add(device);
            }
            else
            {
                device.UpdateToken(token);
            }
        }

        public void RemoveDevice(string deviceName)
        {
            var device = _devices.FirstOrDefault(d => d.Name == deviceName);
            
            if (device != null) _devices.Remove(device);
        }
        
        public void Delete()
        {
            _name = "Deleted user";
            _iconUri = "deleted";
            
            _isDeleted = true;
        }
    }
}