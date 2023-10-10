using Choice.Application.UseCases.Reviews.GetClientReviews;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Reviews.GetClientReviews
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller, IOutputPort
    {
        private readonly IGetClientReviewsUseCase _useCase;

        private IActionResult _viewModel;

        public ReviewController(IGetClientReviewsUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(IList<Review> reviews)
        {
            _viewModel = Ok(reviews);
        }

        [HttpGet("Client/{id}/Get")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(id);

            return _viewModel;
        }
    }
}
