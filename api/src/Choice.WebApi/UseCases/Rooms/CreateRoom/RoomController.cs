using Choice.Application.UseCases.Rooms.CreateRoom;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Rooms.CreateRoom
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : Controller, IOutputPort
    {
        private readonly ICreateRoomUseCase _useCase;

        private IActionResult _viewModel;

        public RoomController(ICreateRoomUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Room room)
        {
            _viewModel = Ok(room);
        }

        void IOutputPort.Invalid()
        {
            _viewModel = BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(string name)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(name);

            return _viewModel;
        }
    }
}
