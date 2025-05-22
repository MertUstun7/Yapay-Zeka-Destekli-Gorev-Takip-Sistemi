using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IWorkerRepository : IGTSBase<Worker>
    {
        // Çalışan bulma ve doğrulama fonksiyonlarının tanımı yapılmıştır.
        Task<Worker> FindByWorkerIdAsync(string workerId);
        Task<Worker> FindByUserIdAsync(string userId);
        Task<bool> WorkerExistsAsync(string workerId);

        // Yöneticiye göre filtreleme fonksiyonunun tanımı yapılmıştır.
        Task<List<Worker>> GetWorkersByManagerAsync(string managerId);

        // İş unvanına göre filtreleme fonksiyonunun tanımı yapılmıştır.
        Task<List<Worker>> GetWorkersByJobTitleAsync(string jobTitle);

        // İlişkili verilerle çalışan çekme fonksiyonunun tanımı yapılmıştır.
        Task<Worker> GetWorkerWithDetailsAsync(string workerId);

        // Görevlerle ilgili sorgular fonksiyonunun tanımı yapılmıştır.
        Task<List<Assignment>> GetWorkerAssignmentsAsync(string workerId);

        // Arama ve filtreleme fonksiyonunun tanımı yapılmıştır.
        Task<List<Worker>> SearchWorkersAsync(string searchTerm);

        // Soft delete ve durum güncelleme fonksiyonunun tanımı yapılmıştır.
        Task DeactivateWorkerAsync(string workerId);

        Task<List<Worker>> GetActiveWorkerListAsync();

        Task<List<Worker>> GetDeactiveWorkerListAsync();

        Task ActivateWorkerAsync(string workerId);
    }
}
