using Choice.ReviewService.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReviewService.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _repository;
        private readonly IHttpContextAccessor _context;

        public ReviewController(IReviewRepository repository, IHttpContextAccessor context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendReview(string guid, )
        {
            string id = _context.HttpContext?.User.FindFirst("id")?.Value!;

            
        }
    }
}
