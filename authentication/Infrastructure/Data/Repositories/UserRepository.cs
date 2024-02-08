using Choice.Authentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace Choice.Authentication.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task Delete(int id)
        {
            await _context
                .Database
                .ExecuteSqlRawAsync($"DELETE FROM Users WHERE Id={id}");
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByPhone(string phone)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Phone == phone);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
