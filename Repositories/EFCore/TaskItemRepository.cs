using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class TaskItemRepository: GTSBase<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(GTSDbContext context) : base(context) { }

        // ID'de belirtilem şirkete ait tüm görevleri, atamaları ve oluşturan kişinin bilgisiyle birlikte getirir.
        public async Task<List<TaskItem>> GetByCompanyIdAsync(Guid companyId, bool trackChanges)
        {
            var query = _context.TaskItems
                .Where(t => t.CompanyId == companyId) 
                .Include(t => t.Assignments)
                .ThenInclude(a => a.AssignedTo)
                .Include(t => t.CreatedBy); 


            return await query.ToListAsync();
        }
       
        // Belirtilen kullanıcı tarafından oluşturulmuş tüm görevleri getirir.
        public async Task<List<TaskItem>> GetByCreatorIdAsync(string creatorId, bool trackChanges)
            => await FindByConditionAsync(t => t.CreatedById == creatorId, trackChanges);

        //Belirtilen kullanıcıya atanmış tüm görevleri ve atama bilgilerini getirir.
        public async Task<List<TaskItem>> GetAssignedToUserAsync(string userId, bool trackChanges)
            => await _context.TaskItems
                     .Where(t => t.Assignments.Any(a => a.AssignedToId == userId))
                     .Include(t => t.Assignments)
                         .ThenInclude(a => a.AssignedTo)
                     .ToListAsync();

        //Belirtilen durum ve şirkete ait görevleri filtreleyip getirir.
        public async Task<List<TaskItem>> GetByStatusAsync(string status, Guid companyId, bool trackChanges)
            => await FindByConditionAsync(t => t.Status == status && t.CompanyId == companyId, trackChanges);
    }
}
