using Administration.Application.Commands.EditReview;
using Administration.Application.Contracts;
using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Modules.Admin.Reviews
{
    [ApiController]
    [Route("api/admin/reviews")]
    public class ReviewController : Controller
    {
        private readonly IAdministrationModule _module;

        public ReviewController(IAdministrationModule module)
        {
            _module = module;
        }
        
        [HttpPut]
        [HasPermission(Permissions.EditReview)]
        public async Task<IActionResult> EditReview([FromBody] EditReviewRequest request)
        {
            await _module.ExecuteCommand(new EditReviewCommand(request.ReviewId, request.Text, request.Grade));
            
            return Ok();
        }
    }
}