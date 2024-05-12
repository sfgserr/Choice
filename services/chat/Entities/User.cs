namespace Choice.Chat.Api.Entities
{
    public class User
    {
        public User(string guid, string name, string iconUri)
        {
            Guid = guid;
            Name = name;
            IconUri = iconUri;
        }

        protected User() { }

        public string Guid { get; }
        public string Name { get; private set; }
        public string IconUri { get; private set; }

        public void ChangeIconUri(string iconUri)
        {
            if (!string.IsNullOrEmpty(iconUri))
                IconUri = iconUri;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
