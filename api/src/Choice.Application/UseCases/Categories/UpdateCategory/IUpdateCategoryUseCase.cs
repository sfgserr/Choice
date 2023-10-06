using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.UpdateCategory
{
    public interface IUpdateCategoryUseCase
    {
        Task Execute(Category category);

        void SetOutputPort(IOutputPort outputPort);
    }
}
