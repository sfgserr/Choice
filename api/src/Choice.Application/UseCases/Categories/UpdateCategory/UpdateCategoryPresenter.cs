using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.UpdateCategory
{
    public class UpdateCategoryPresenter : IOutputPort
    {
        public Category? Category { get; set; }

        public void Ok(Category category)
        {
            Category = category;
        }
    }
}
