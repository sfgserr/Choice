using Choice.Domain;
using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly ChoiceContext _context;

        public RoomRepository(ChoiceContext context)
        {
            _context = context;
        }

        public async Task<Room> Create(Room entity)
        {
            await _context.Rooms.AddAsync(entity);

            return entity;
        }

        public async Task Delete(Room entity)
        {
            await _context
                .Database
                .ExecuteSqlRawAsync($"DELETE FROM Rooms WHERE Id={entity.Id}");
        }

        public async Task<IList<Room>> Get()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetBy(Func<Room, bool> func)
        {
            List<Room> rooms = await _context.Rooms.ToListAsync();

            return rooms.FirstOrDefault(c => func(c));
        }

        public async Task<Room> Update(Room entity)
        {
            await Task.Run(() =>
            {
                _context.Rooms.Update(entity);
            });

            return entity;
        }
    }
}
