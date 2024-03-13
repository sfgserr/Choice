using Choice.EventBus.Messages.Events;
using Choice.ReviewService.Api.Entities;
using Choice.ReviewService.Api.Repositories;
using Choice.ReviewService.Api.ViewModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Choice.ReviewService.Api.Services;
using Choice.Ordering.Grpc.Protos;

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

            CanSendReviewResponse response = await _orderingService.CanSendReview(viewModel.Guid);

            if (!response.Result)
                return BadRequest();

            Review review = new
                (id,
                 viewModel.Guid,
                 viewModel.PhotoUris, 
                 viewModel.Text, 
                 viewModel.Grade);

            await _repository.Add(review);

            await _endPoint.Publish(new ReviewLeftEvent(response.Id, id, viewModel.Grade));

            return Ok(review);
        }
    }
}
