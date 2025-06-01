using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ITaskItemRepository: IGTSBase<TaskItem>
    {
        Task<List<TaskItem>> GetByCompanyIdAsync(Guid companyId, bool trackChanges);
        Task<List<TaskItem>> GetByCreatorIdAsync(string creatorId, bool trackChanges);
        Task<List<TaskItem>> GetAssignedToUserAsync(string userId, bool trackChanges);
        Task<List<TaskItem>> GetByStatusAsync(string status, Guid companyId, bool trackChanges);
    }
}
