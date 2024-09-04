using Microsoft.EntityFrameworkCore;
using Users.Domain.Users;

namespace Users.Infrastructure.Data.Domain.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly UsersContext _usersContext;

        internal UserRepository(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task Add(User user)
        {
            await _usersContext.Users.AddAsync(user);
        }

        public async Task<IList<User>> GetAll()
        {
            return await _usersContext.Users.ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _usersContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> Get(UserId id)
        {
            return await _usersContext.Users.FindAsync(id);
        }
    }
}