namespace Choice.CategoryService.Api.ViewModels
{
    public class UpdateCategoryRequest
    {
        public UpdateCategoryRequest(int id, string title, string iconUri)
        {
            Id = id;
            Title = title;
            IconUri = iconUri;
        }

        public int Id { get; }
        public string Title { get; }
        public string IconUri { get; }
    }
}
