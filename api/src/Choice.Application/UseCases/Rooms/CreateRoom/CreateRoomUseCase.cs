using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Rooms.CreateRoom
{
    public class CreateRoomUseCase : ICreateRoomUseCase
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public CreateRoomUseCase(IRepository<Room> roomRepository, IUnitOfWork unitOfWork)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new CreateRoomPresenter();
        }

        public async Task Execute(string name)
        {
            Room room = new Room()
            {
                Name = name
            };

            Room createdRoom = await _roomRepository.Create(room);

            await _unitOfWork.Save();

            _outputPort.Ok(createdRoom);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
