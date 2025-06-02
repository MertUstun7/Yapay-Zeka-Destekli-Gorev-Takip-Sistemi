using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IReportRepository:IGTSBase<TaskReport>
    {

        Task<List<TaskReport>> GetReportByIdAsync(Guid taskId, bool trackChanges);
        Task<List<TaskReport>> GetReportByUserIdAsync(string userId, bool trackChanges);

        Task<List<TaskReport>> GetReportsByCompanyIdAsync(Guid companyId, bool trackChanges);

    }
}
