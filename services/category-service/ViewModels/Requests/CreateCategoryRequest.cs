namespace Choice.CategoryService.Api.ViewModels.Requests
{
    public class CreateCategoryRequest
    {
        public CreateCategoryRequest(string title, string iconUri)
        {
            Title = title;
            IconUri = iconUri;
        }

        public string Title { get; }
        public string IconUri { get; }
    }
}
