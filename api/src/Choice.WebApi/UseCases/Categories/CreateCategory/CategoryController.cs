using Choice.Application.UseCases.Categories.CreateCategory;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Categories.CreateCategory
{
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : Controller, IOutputPort
    {
        private readonly ICreateCategoryUseCase _useCase;

        private IActionResult _viewModel;

        public CategoryController(ICreateCategoryUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Category category)
        {
            _viewModel = Ok(category);
        }

        void IOutputPort.Invalid()
        {
            _viewModel = BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(string title, string iconUri)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(title, iconUri);

            return _viewModel;
        }
    }
}
