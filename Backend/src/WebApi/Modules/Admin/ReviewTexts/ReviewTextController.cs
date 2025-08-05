using Administration.Application.Commands.AddReviewText;
using Administration.Application.Commands.DeleteReviewText;
using Administration.Application.Contracts;
using Administration.Application.Queries.GetReviewTexts;
using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Modules.Admin.ReviewTexts
{
    [ApiController]
    [Route("api/reviewTexts")]
    public class ReviewTextController : Controller
    {
        private readonly IAdministrationModule _module;

        public ReviewTextController(IAdministrationModule module)
        {
            _module = module;
        }
        
        [HttpPost]
        [HasPermission(Permissions.AddReviewText)]
        public async Task<IActionResult> Add([FromBody] AddReviewTextRequest request)
        {
            await _module.ExecuteCommand(new AddReviewTextCommand(
                request.Grade,
                request.Text));
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeleteReviewText)]
        public async Task<IActionResult> Delete(int id)
        {
            await _module.ExecuteCommand(new DeleteReviewTextCommand(id));
            
            return Ok();
        }
        
        [HttpGet]
        [HasPermission(Permissions.GetReviewTexts)]
        public async Task<IActionResult> Get()
        {
            var reviewTexts = await _module
                .Query<GetReviewTextsQuery, IEnumerable<ReviewTextDto>>(
                    new GetReviewTextsQuery());
            
            return Ok(reviewTexts);
        }
    }
}