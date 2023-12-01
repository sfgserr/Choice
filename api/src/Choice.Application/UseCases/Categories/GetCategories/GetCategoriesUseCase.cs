using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.GetCategories
{
    public class GetCategoriesUseCase : IGetCategoriesUseCase
    {
        private readonly IRepository<Category> _categoryRepository;

        private IOutputPort _outputPort;

        public GetCategoriesUseCase(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;

            _outputPort = new GetCategoriesPresenter();
        }

        public async Task Execute() =>
            await GetCategories();

        private async Task GetCategories()
        {
            IList<Category> categories = await _categoryRepository.Get();

            _outputPort.Ok(categories);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
