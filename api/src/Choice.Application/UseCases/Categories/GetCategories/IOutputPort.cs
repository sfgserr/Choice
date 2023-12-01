using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.GetCategories
{
    public interface IOutputPort
    {
        void Ok(IList<Category> categories);
    }
}
