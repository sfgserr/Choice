namespace Choice.Chat.Api.Entities
{
    public class Group
    {
        public Group(string userGuid, string userIconUri)
        {
            UserGuid = userGuid;
            UserIconUri = userIconUri;
        }

        public string UserGuid { get; }
        public string UserIconUri { get; private set; }

        public void ChangeIconUri(string iconUri)
        {
            if (!string.IsNullOrEmpty(iconUri))
                UserIconUri = iconUri;
        }
    }
}
