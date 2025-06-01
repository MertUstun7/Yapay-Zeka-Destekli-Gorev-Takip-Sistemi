using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ITaskService
    {
        Task UpdateTaskStatusAsync(Guid taskId, string status);
        Task<IEnumerable<TaskItemForGetDto>> GetAllAsync();
        Task<TaskItemForGetDto> GetTaskByIdAsync(Guid taskId);
        Task<TaskItemForGetDto> CreateTaskAsync(TaskItemDtoForCreate taskDto);
        Task UpdateTaskAsync(Guid taskId, TaskItemForUpdateDto taskDto);
        Task DeleteTaskAsync(Guid taskId);
        Task<IEnumerable<TaskItemForGetDto>> GetByCompanyAsync();
        Task<IEnumerable<TaskItemForGetDto>> GetByCreatorAsync();
        Task<IEnumerable<TaskItemForGetDto>> GetAssignedToCurrentUserAsync();
        Task<IEnumerable<TaskItemForGetDto>> GetByStatusAsync(string status);   
        Task<IEnumerable<UserDtoForGet>> GetAssigneesAsync(Guid taskId);
    }
}
