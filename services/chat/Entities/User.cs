namespace Choice.Chat.Api.Entities
{
    public class User
    {
        public User(string guid, string iconUri)
        {
            Guid = guid;
            IconUri = iconUri;
        }

        public string Guid { get; }
        public string IconUri { get; private set; }

        public void ChangeIconUri(string iconUri)
        {
            if (!string.IsNullOrEmpty(iconUri))
                IconUri = iconUri;
        }
    }
}
