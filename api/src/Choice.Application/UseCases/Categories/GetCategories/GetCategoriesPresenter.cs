using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.GetCategories
{
    public class GetCategoriesPresenter : IOutputPort
    {
        public IList<Category>? Categories { get; set; } 

        public void Ok(IList<Category> categories)
        {
            Categories = categories;
        }
    }
}
