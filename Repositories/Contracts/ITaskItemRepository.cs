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
        // trackChanges parametresi ile EF Core'un değişiklikleri izleyip izlemeyeceği belirtilir.

        // Şirket içerisinde bulunan tüm görevleri, bu görevlere ait atamaları ve görevi oluşturan kişinin bilgilerini getiren asenkron metot tanımıdır.
        Task<List<TaskItem>> GetByCompanyIdAsync(Guid companyId, bool trackChanges);

        // Kullanıcının (şirket bünyesindeki kişi) oluşturduğu tüm görevleri getiren asenkron metot tanımıdır. 
        Task<List<TaskItem>> GetByCreatorIdAsync(string creatorId, bool trackChanges);

        // Kullanıcıya (şirket bünyesindeki kişi) atanmış tüm görevleri ve görev atama detaylarını getiren asenkron metot tanımıdır.
        Task<List<TaskItem>> GetAssignedToUserAsync(string userId, bool trackChanges);

        // Belirtilen duruma sahip şirket bünyesindeki görevleri getiren asenkron metot tanımıdır.
        Task<List<TaskItem>> GetByStatusAsync(string status, Guid companyId, bool trackChanges);
    }
}
