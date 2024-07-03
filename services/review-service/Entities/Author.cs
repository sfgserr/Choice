namespace Choice.ReviewService.Api.Entities
{
    public class Author
    {
        public Author(string guid, string name, string iconUri)
        {
            Guid = guid;
            Name = name;
            IconUri = iconUri;
        }

        public string Guid { get; }
        public string Name { get; private set; }
        public string IconUri { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeIconUri(string iconUri)
        {
            IconUri = iconUri;
        }

        public void Delete()
        {
            IsDeleted = true;
            Name = "Удаленный аккаунт";
            IconUri = "deleted-png";
        }
    }
}
