namespace Choice.Chat.Api.Entities
{
    public class User
    {
        public User(string guid, string name, string iconUri, string deviceToken)
        {
            Guid = guid;
            Name = name;
            IconUri = iconUri;

            DeviceTokens.Add(deviceToken);
        }

        protected User() { }

        public string Guid { get; }
        public string Name { get; private set; }
        public string IconUri { get; private set; }
        public UserStatus Status { get; private set; } = UserStatus.Offline;
        public DateTime? LastTimeOnline { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public List<string> DeviceTokens { get; private set; } = [];

        public void ChangeIconUri(string iconUri)
        {
            if (!string.IsNullOrEmpty(iconUri))
                IconUri = iconUri;
        }

        public void Delete()
        {
            IsDeleted = true;
            IconUri = "deleted-png";
            Name = "Удаленный аккаунт";
        }

        public void SetStatus(UserStatus status)
        {
            if (status == UserStatus.Offline)
                LastTimeOnline = DateTime.Now;

            Status = status;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }

    public enum UserStatus
    {
        Online = 1,
        Offline = 2
    }
}
