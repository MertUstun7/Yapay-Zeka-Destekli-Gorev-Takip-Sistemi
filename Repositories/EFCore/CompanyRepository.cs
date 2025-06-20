using Entities;
using Entities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class CompanyRepository : GTSBase<Company>,ICompanyRepository
    {
        public CompanyRepository(GTSDbContext context) : base(context)
        {
        }

        //Verilen şirket ID'sine bağlı personelleri listelememizi sağlar.
        public async Task<Company> GetWithUsersAsync(Guid companyId, bool trackChanges)
        {    
            var query = _context.Companies
                         .Include(c => c.Users)
                         .Where(c => c.Id == companyId);

            if (!trackChanges)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync()
                   ?? throw new CompanyNotFoundException($"{companyId} şirketi bulunamadı.");
        }


        // İlgili ID'de yer alan şirketin verilerini görüntülenmesini sağlar.
        public async Task<Company> GetByIdAsync(Guid id, bool trackChanges)
        {
            IQueryable<Company> query = _context.Companies.Where(c => c.Id == id);

            if(!trackChanges)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync()
                ?? throw new CompanyNotFoundException($"{id} şirketi bulunamadı.");

        }
   

    }
}

