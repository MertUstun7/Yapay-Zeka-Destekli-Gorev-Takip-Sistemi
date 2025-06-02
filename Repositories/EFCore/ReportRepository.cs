using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ReportRepository :GTSBase<TaskReport> ,IReportRepository
    {
        public ReportRepository(GTSDbContext context):base(context) { }

        //Belirtilen görev ID'sine ait tüm raporları getirir; isteğe bağlı olarak değişiklik takibini kapatıp açılabilir.
        public async Task<List<TaskReport>> GetReportByIdAsync(Guid taskId, bool trackChanges)
        {
            var query = _context.TaskReports
                .Include(r => r.CreatedBy).
                Where(r => r.TaskItemId == taskId);

            if (!trackChanges)
                query=query.AsNoTracking();

            return await query.ToListAsync();

        }

        // İlgili ID'ye sahip kullanıcının oluşturmuş olduğu tüm raporları getirir. İsteğe göre değişiklik takibini açıp kapatabilir.
        public async Task<List<TaskReport>> GetReportByUserIdAsync(string userId, bool trackChanges)
        {
            var query = _context.TaskReports.
                    Include(r => r.TaskItem).
                    Where(r => r.CreatedById == userId);

            if (!trackChanges)
            {
                query=query.AsNoTracking();
            }

            return await query.ToListAsync();
        }
        // Belirtilen şirket ID'sine ait görevlerin tüm raporlarını getirir, isteğe bağlı olarak değişiklik takibini açıp katabilir.
        public async Task<List<TaskReport>> GetReportsByCompanyIdAsync(Guid companyId, bool trackChanges)
        {
            var query = _context.TaskReports
            .Include(r => r.CreatedBy)
            .Include(r => r.TaskItem)
            .Where(r => r.TaskItem.CompanyId == companyId);

            if (!trackChanges)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
