using Interview_Api.Data;
using Interview_Api.Models;
using Interview_Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Interview_Api.Repositories
{
    public class UserRepository     :IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context) {              
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }          

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() >0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
