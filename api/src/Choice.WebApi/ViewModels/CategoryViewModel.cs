using Choice.Domain.Models;

namespace Choice.WebApi.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel(Category category)
        {
            Title = category.Title;
            IconUri = category.IconUri;
        }

        public string Title { get; set; }
        public string IconUri { get; set; }
    }
}
