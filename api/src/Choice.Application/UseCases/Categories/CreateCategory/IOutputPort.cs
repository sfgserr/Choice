using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.CreateCategory
{
    public interface IOutputPort
    {
        void Ok(Category category);

        void Invalid();
    }
}
