using Administration.Application.Commands.CreateCategory;
using Administration.Application.Commands.EditCategory;
using Administration.Application.Contracts;
using Administration.Application.Queries.GetCategories;
using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Modules.Admin.Categories
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly IAdministrationModule _adminModule;

        public CategoryController(IAdministrationModule adminModule)
        {
            _adminModule = adminModule;
        }
        
        [HttpPost]
        [HasPermission(Permissions.CreateCategory)]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            await _adminModule.ExecuteCommand(new CreateCategoryCommand(
                request.Title,
                request.IconUri));

            return Ok();
        }
        
        [HttpPut]
        [HasPermission(Permissions.EditCategory)]
        public async Task<IActionResult> Edit(EditCategoryRequest request)
        {
            await _adminModule.ExecuteCommand(new EditCategoryCommand(
                request.CategoryId,
                request.Title,
                request.IconUri));

            return Ok();
        }
        
        [HttpGet]
        [HasPermission(Permissions.GetCategories)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _adminModule
                .Query<GetCategoriesQuery, IEnumerable<CategoryDto>>(new GetCategoriesQuery());

            return Ok(categories);
        }
    }
}