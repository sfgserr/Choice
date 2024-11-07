namespace WebApi.Modules.Admin.Categories
{
    public class EditCategoryRequest
    {
        public EditCategoryRequest(int categoryId, string title, string iconUri)
        {
            CategoryId = categoryId;
            Title = title;
            IconUri = iconUri;
        }
        
        public int CategoryId { get; }
        
        public string Title { get; }
        
        public string IconUri { get; }
    }
}