using System.Threading.Tasks;
using AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> FindAsync(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(login));
            return user;
        }
        
        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        
        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}