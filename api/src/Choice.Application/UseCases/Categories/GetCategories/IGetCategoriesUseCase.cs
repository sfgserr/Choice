
namespace Choice.Application.UseCases.Categories.GetCategories
{
    public interface IGetCategoriesUseCase
    {
        Task Execute();

        void SetOutputPort(IOutputPort outputPort);
    }
}
