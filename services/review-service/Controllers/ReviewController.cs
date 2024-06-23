using Choice.EventBus.Messages.Events;
using Choice.ReviewService.Api.Entities;
using Choice.ReviewService.Api.Repositories;
using Choice.ReviewService.Api.ViewModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Choice.ReviewService.Api.Services;
using ReviewService.Api.Controllers;

namespace Choice.ReviewService.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _repository;
        private readonly OrderingService _orderingService;
        private readonly IHttpContextAccessor _context;
        private readonly IPublishEndpoint _endPoint;

        public ReviewController(IReviewRepository repository, IHttpContextAccessor context,
            OrderingService orderingService, IPublishEndpoint endPoint)
        {
            _repository = repository;
            _context = context;
            _orderingService = orderingService;
            _endPoint = endPoint;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendReview(CreateReviewViewModel viewModel)
        {
            string id = _context.HttpContext?.User.FindFirst("id")?.Value!;

            if (id == viewModel.Guid)
                return BadRequest();

            bool result = await _orderingService.AddReview(id, viewModel.Guid);

            if (!result)
                return BadRequest();

            Review review = new
                (id,
                 viewModel.Guid,
                 viewModel.PhotoUris, 
                 viewModel.Text, 
                 viewModel.Grade);

            await _repository.Add(review);

            await _endPoint.Publish(new ReviewLeftEvent(viewModel.Guid, viewModel.Grade));

            return Ok(review);
        }

        [HttpPut("Edit")]
        [Authorize("Admin")]
        public async Task<IActionResult> EditReview(EditReviewRequest request)
        {
            Review review = await _repository.Get(request.Id);

            if (review is not null)
            {
                review.Edit(request.Grade, request.Text, request.PhotoUris);

                await _repository.Update(review);

                return Ok(review);
            }

            return NotFound();
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetReviews(string guid)
        {
            IList<Review> reviews = await _repository.Get(guid);

            return Ok(reviews);
        }
    }
}
