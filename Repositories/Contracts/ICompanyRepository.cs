using Repositories.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICompanyRepository:IGTSBase<Company>
    {
        Task<Company> GetWithUsersAsync(Guid companyId, bool trackChanges);

        Task<Company> GetWithTasksAsync(Guid companyId, bool trackChanges);

        public Task<Company> GetByIdAsync(Guid id, bool trackChanges);
    }
}
