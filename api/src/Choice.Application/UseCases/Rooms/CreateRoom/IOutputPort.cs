using Choice.Domain.Models;

namespace Choice.Application.UseCases.Rooms.CreateRoom
{
    public interface IOutputPort
    {
        void Ok(Room room);

        void Invalid();
    }
}
