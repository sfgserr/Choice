using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Infrastructure.Repositories.Fakes
{
    public class RoomRepositoryFake : IRepository<Room>
    {
        private readonly ChoiceContextFake _context;

        public RoomRepositoryFake(ChoiceContextFake context)
        {
            _context = context;
        }

        public async Task<Room> Create(Room entity)
        {
            _context.Rooms.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task Delete(Room entity)
        {
            Room entityToRemove = _context.Rooms.FirstOrDefault(c => c.Id == entity.Id);

            if (entityToRemove is null)
            {
                return;
            }

            _context.Rooms.Remove(entityToRemove);
            await Task.CompletedTask;
        }

        public async Task<IList<Room>> Get()
        {
            IList<Room> rooms = _context.Rooms.ToList();

            return await Task.FromResult(rooms);
        }

        public async Task<Room> GetBy(Func<Room, bool> func)
        {
            Room room = _context.Rooms.FirstOrDefault(c => func(c));

            return await Task.FromResult(room);
        }

        public async Task<Room> Update(Room entity)
        {
            Room oldRoom = _context.Rooms.FirstOrDefault(c => c.Id == entity.Id);

            if (oldRoom != null)
            {
                _context.Rooms.Remove(oldRoom);
            }

            _context.Rooms.Add(entity);

            return await Task.FromResult(entity);
        }
    }
}
