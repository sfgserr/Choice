
namespace Choice.Application.UseCases.Categories.CreateCategory
{
    public interface ICreateCategoryUseCase
    {
        Task Execute(string title, string iconUri);

        void SetOutputPort(IOutputPort outputPort);
    }
}
