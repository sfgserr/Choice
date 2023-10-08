using Choice.Application.UseCases.Categories.UpdateCategory;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Categories.UpdateCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller, IOutputPort
    {
        private readonly IUpdateCategoryUseCase _useCase;

        private IActionResult _viewModel;

        public CategoryController(IUpdateCategoryUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Category category)
        {
            _viewModel = Ok(category);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Category category)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(category);

            return _viewModel;
        }
    }
}
