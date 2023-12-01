using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.CreateCategory
{
    public class CreateCategoryPresenter : IOutputPort
    {
        public bool IsInvalid { get; set; } = false;
        public Category? Category { get; set; }

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void Ok(Category category)
        {
            Category = category;
        }
    }
}
