using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Infrastructure.Data;
using Choice.Chat.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Choice.Chat.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDdContext _context;

        public UserRepository(ChatDdContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<User>> GetAll() =>
            await _context.Users.ToListAsync();

        public async Task<User> Get(string guid) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Guid == guid);

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(string id)
        {
            int affections = await _context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Guid = @p0", id);

            return affections > 0;
        }
    }
}
