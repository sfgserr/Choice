
namespace Choice.CategoryService.Api.ViewModels
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
