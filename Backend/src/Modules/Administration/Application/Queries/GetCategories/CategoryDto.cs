namespace Administration.Application.Queries.GetCategories
{
    public class CategoryDto
    {
        public CategoryDto(int categoryId, string title, string iconUri)
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