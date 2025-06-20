using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    // Görev işlemlerini yöneten servisin arayüzüdür.
    public interface ITaskService
    {
        // Görev durumunu güncelleyen metodun tanımı yapılmıştır.
        Task UpdateTaskStatusAsync(Guid taskId, string status);

        // Tüm görevleri listeleyen metodun tanımı yapılmıştır.
        Task<IEnumerable<TaskItemForGetDto>> GetAllAsync();

        // İlgili ID'de yer alan görevi getiren metodun tanımı yapılmıştır.
        Task<TaskItemForGetDto> GetTaskByIdAsync(Guid taskId);

        // Görev oluşturan metodun tanımı yapılmıştır.
        Task<TaskItemForGetDto> CreateTaskAsync(TaskItemDtoForCreate taskDto);

        // Görev ile ilgili verileri güncelleyen metodun tanımı yapılmıştır.
        Task UpdateTaskAsync(Guid taskId, TaskItemForUpdateDto taskDto);

        // Görevi silen metodun tanımı yapılmıştır.
        Task DeleteTaskAsync(Guid taskId);

        // Giriş yapan kullanıcının şirketine ait görevleri getiren metodun tanımı yapılmıştır.
        Task<IEnumerable<TaskItemForGetDto>> GetByCompanyAsync();

        // Giriş yapan kullanıcının oluşturduğu görevleri getiren metodun tanımı yapılmıştır.

        Task<IEnumerable<TaskItemForGetDto>> GetByCreatorAsync();

        // Giriş yapan kullanıcıya atanan görevleri getiren metodun tanımı yapılmıştır.

        Task<IEnumerable<TaskItemForGetDto>> GetAssignedToCurrentUserAsync();

        // Verilen duruma ait görevleri getiren metodun tanımı yapılmıştır.
        Task<IEnumerable<TaskItemForGetDto>> GetByStatusAsync(string status);   

        // Belirli göreve atanmış kullanıcıları getiren metodun tanımı yapılmıştır.
        Task<IEnumerable<UserDtoForGet>> GetAssigneesAsync(Guid taskId);
        // Görev durumunu güncelleyen metodun tanımı yapılmıştır.
        Task UpdateStatus(Guid taskId, JsonPatchDocument<TaskItem> taskDto);
    }
}
