using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Categories.CreateCategory
{
    public class CreateCategoryUseCase : ICreateCategoryUseCase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public CreateCategoryUseCase(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new CreateCategoryPresenter();
        }

        public async Task Execute(string title, string iconUri)
        {
            Category category = new Category()
            {
                Title = title,
                IconUri = iconUri
            };

            Category createdCategory = await _categoryRepository.Create(category);

            await _unitOfWork.Save();

            _outputPort.Ok(createdCategory); 
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
