namespace WebApi.Modules.Admin.Categories
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