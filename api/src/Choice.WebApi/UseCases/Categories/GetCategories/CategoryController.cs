using Choice.Application.UseCases.Categories.GetCategories;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Categories.GetCategories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller, IOutputPort
    {
        private readonly IGetCategoriesUseCase _useCase;

        private IActionResult _viewModel;

        public CategoryController(IGetCategoriesUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(IList<Category> categories)
        {
            _viewModel = Ok(categories);
        }

        [Route("Get")]
        [HttpGet()]
        public async Task<IActionResult> GetCategories()
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute();

            return _viewModel;
        }
    }
}
