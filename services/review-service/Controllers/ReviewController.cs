using Choice.ReviewService.Api.Entities;
using Choice.ReviewService.Api.Infrastructure.Ordering;
using Choice.ReviewService.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ReviewService.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _repository;
        private readonly IOrderingService _orderingService;
        private readonly IHttpContextAccessor _context;

        public ReviewController(IReviewRepository repository, IHttpContextAccessor context,
            IOrderingService orderingService)
        {
            _repository = repository;
            _context = context;
            _orderingService = orderingService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendReview(string guid, string text, int grade, List<string> photoUris)
        {
            string id = _context.HttpContext?.User.FindFirst("id")?.Value!;

            bool canSendReview = await _orderingService.CanSendReview(id);

            if (!canSendReview)
                return BadRequest();

            Review review = new(id, guid, photoUris, text, grade);

            await _repository.Add(review);

            return Ok(review);
        }
    }
}
