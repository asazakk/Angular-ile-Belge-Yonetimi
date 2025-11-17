using Microsoft.EntityFrameworkCore;
using e_Ticaret.Domain.Entities;
using e_Ticaret.Domain.Interfaces;
using e_Ticaret.Infrastructure.Data;

namespace e_Ticaret.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EticaretDbContext context) : base(context)
        {
        }
        
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Username == username && !u.IsDeleted);
        }
        
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }
        
        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await _dbSet
                .AnyAsync(u => u.Username == username && !u.IsDeleted);
        }
        
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _dbSet
                .AnyAsync(u => u.Email == email && !u.IsDeleted);
        }
        
        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _dbSet
                .Where(u => u.IsActive && !u.IsDeleted)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<User>> GetUsersByDepartmentAsync(string department)
        {
            return await _dbSet
                .Where(u => u.Department == department && u.IsActive && !u.IsDeleted)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();
        }
    }
}
