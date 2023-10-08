using Choice.Domain.Models;

namespace Choice.Application.UseCases.Rooms.CreateRoom
{
    public class CreateRoomPresenter : IOutputPort
    {
        public bool IsInvalid { get; set; } = false;
        public Room? Room { get; set; }

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void Ok(Room room)
        {
            Room = room;
        }
    }
}
