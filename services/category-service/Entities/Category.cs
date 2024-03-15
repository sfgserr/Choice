namespace Choice.CategoryService.Api.Entities
{
    public class Category
    {
        public Category(string title, string iconUri)
        {
            Title = title;
            IconUri = iconUri;
        }

        private Category() { }

        public int Id { get; }
        public string Title { get; private set; }
        public string IconUri { get; private set; }

        public void Update(string title, string iconUri)
        {
            Title = title;
            IconUri = iconUri;
        }
    }
}
