using Choice.Authentication.Data;
using Choice.Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Authentication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();  
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByPhoneNumber(string phoneNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<bool> Update(User user)
        {
            _context.Users.Update(user);

            int affections = await _context.SaveChangesAsync();

            return affections > 0;
        }
    }
}
