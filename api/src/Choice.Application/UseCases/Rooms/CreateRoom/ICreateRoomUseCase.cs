
namespace Choice.Application.UseCases.Rooms.CreateRoom
{
    public interface ICreateRoomUseCase
    {
        Task Execute(string name);

        void SetOutputPort(IOutputPort outputPort);
    }
}
