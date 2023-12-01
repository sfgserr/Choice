using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.UpdateCategory
{
    public interface IOutputPort
    {
        void Ok(Category category);
    }
}
