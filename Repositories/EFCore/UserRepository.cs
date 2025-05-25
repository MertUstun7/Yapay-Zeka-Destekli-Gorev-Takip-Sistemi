using Entities.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    
   
    public class UserRepository : GTSBase<User>, IUserRepository
    {
        public UserRepository(GTSDbContext context) : base(context) { }

        public async Task<IEnumerable<User>> GetUsersByCompanyIdAsync(Guid companyId, bool trackChanges)
        {
            IQueryable<User> query = _context.Users.Where(u => u.CompanyId == companyId);
            if (!trackChanges)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchByNameAsync(string name, bool trackChanges)
        {
            IQueryable<User> query = _context.Users
                .Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name));

            if (!trackChanges)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<User> GetUserWithCompanyAsync(string userId, bool trackChanges)
        {
            IQueryable<User> query = _context.Users
                .Include(u => u.Company)
                .Where(u => u.Id == userId);

            if (!trackChanges)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync()
                ?? throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

    }
    
}
