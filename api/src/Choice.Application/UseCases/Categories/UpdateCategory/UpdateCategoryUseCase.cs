using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.UpdateCategory
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public UpdateCategoryUseCase(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new UpdateCategoryPresenter();
        }

        public async Task Execute(Category category) =>
            await UpdateCategory(category);

        private async Task UpdateCategory(Category category)
        {
            Category updatedCategory = await _categoryRepository.Update(category);

            await _unitOfWork.Save();

            _outputPort.Ok(updatedCategory);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
