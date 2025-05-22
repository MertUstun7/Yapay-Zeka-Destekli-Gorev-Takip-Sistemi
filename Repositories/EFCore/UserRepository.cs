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

       
        

        public async Task<bool> EmailExistsAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == email);
        }

        // Kullanıcı türüne göre filtreleme
       

        // İlişkili verilerle kullanıcı çekme
        
        public async Task<User> GetUserWithDetailsAsync(string userId)
        {
            return  await
                _context.Users
                .Include(u => u.Manager)
                .Include(u => u.Worker)
                .Include(u => u.Reports)
                .FirstOrDefaultAsync(u => u.Id == userId.ToString());
        }
        
        // Durum bazlı sorgular
        public async Task<List<User>> GetActiveUsersAsync()
        {
            return await FindByConditionAsync(u => u.IsActive, false);
        }

        // Görev ve raporlarla ilgili sorgular
        public async Task<List<Assignment>> GetUserAssignmentsAsync(string userId)
        {
            return await _context.Assignments
                .Where(a => a.AssignedTo == userId)
                .Include(a => a.AssignedWorker)
                .Include(a => a.AssignedManager)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Report>> GetUserReportsAsync(string userId)
        {
            return await _context.Reports
                .Where(r => r.UploadedBy == userId)
                .Include(r => r.Uploader)
                .Include(r => r.Task)
                .AsNoTracking()
                .ToListAsync();
        }

        // Arama ve filtreleme
        public async Task<List<User>> SearchUsersAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await FindAllAsync(false);

            searchTerm = searchTerm.ToLower();
            return await _context.Users
                .Where(u => u.FirstName.ToLower().Contains(searchTerm) ||
                            u.LastName.ToLower().Contains(searchTerm) ||
                            u.Email.ToLower().Contains(searchTerm))
                .AsNoTracking()
                .ToListAsync();
        }

        // Soft delete ve durum güncelleme
        public async Task DeactivateUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            user.IsActive = false;
            
            await _context.SaveChangesAsync();
        }

        public async Task ActivateUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            user.IsActive = true;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            await DeleteAsync(user);
            
            await _context.SaveChangesAsync();
        }

        public IQueryable<User> GetAllQuery(bool trackChanges)
        {
            var query = _context.Users.AsQueryable();
            if (!trackChanges)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<bool> CheckUserId(string userId)
        {
            return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == userId.ToString());
        }

    }
    
}
