using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IWorkerService
    {
        //Aşağıda temel CRUD işlemlerinin interface tanımı yapılmıştır.
        Task<List<WorkerDtoForList>> GetAllWorkersAsync();
        Task<WorkerDtoForDetails> GetWorkerByIdAsync(string workerID);
        Task CreateWorkerAsync(WorkerDtoForCreate workerDto);

        Task UpdateWorkerAsync(string workerId, WorkerDtoForUpdate workerDto);

        Task DeleteWorkerAsync(string workerId);

        // Filtreleme işlemlerinin interface tanımı yapılmıştır.
        Task <List<WorkerDtoForList>> GetWorkersByManagerAsync(string managerId);
        Task<List<WorkerDtoForList>> GetWorkersByJobTitleAsync(string jobTitle);

        Task<List<WorkerDtoForList>> GetActiveWorkerAsync();

        Task<List<WorkerDtoForList>> GetInactiveWorkerAsync();


        //Kontrol işlemleri tanımlanmıştır.

        Task<bool> WorkerExistAsync(string workerId);
        Task<WorkerDtoForDetails> GetWorkerWithDetailAsync(string workerId);


        //Çalışan aktif ve pasif etme işlemleri tanımlanmıştır.

        Task ActiveWorkerAsync(string workerId);

        Task<List<WorkerDtoForList>> DeactiveWorkerAsync(string workerId);

        // Arama işlemi

        Task<List<WorkerDtoForList>> SearchWorkerAsync(string searchTerm);

      
        
        // Yönetici güncelleme işlemi tanımlanmıştır.

        Task ChangeWorkerManagerAsync(string workerId, string newManagerId);



    }
}
