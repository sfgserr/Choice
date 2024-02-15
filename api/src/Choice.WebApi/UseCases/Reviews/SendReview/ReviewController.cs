using Choice.Application.UseCases.Reviews.SendReview;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Reviews.SendReview
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : Controller, IOutputPort
    {
        private readonly ISendReviewUseCase _useCase;

        private IActionResult _viewModel;

        public ReviewController(ISendReviewUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Review review)
        {
            _viewModel = Ok(review);
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send(Review review)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(review.Client, review.Company, review.Grade, review.PhotoUris, review.Text);

            return _viewModel;
        }
    }
}
