using Choice.CategoryService.Api.ViewModels;
using Choice.CategoryService.Api.Entities;
using Choice.CategoryService.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.CategoryService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [Authorize("Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            Category category = new
                (request.Title,
                 request.IconUri);

            await _repository.Add(category);

            return Ok(category);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            IList<Category> categories = await _repository.GetAll();

            return Ok(categories);
        }

        [Authorize("Admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            Category category = await _repository.Get(request.Id);

            if (category is null)
                return NotFound();

            category.Update(request.Title, request.IconUri);

            bool result = await _repository.Update(category);

            return result ? Ok(request) : BadRequest();
        }

        [Authorize("Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);

            return Ok();
        }
    }
}
